using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using equipment_lease_api.DTO;
using equipment_lease_api.Entities;
using equipment_lease_api.Models;
using Microsoft.EntityFrameworkCore;
using static equipment_lease_api.Helpers.Constants.Strings;

namespace equipment_lease_api.Services
{
    public static class LeaseUpdateQuery
    {
        public static object GetLeaseUpdates(string leaseId)
        {
            using (var dbContext = new AppDataContext())
            {
                var lease = (from assetLease in dbContext.AssetLeases
                             where assetLease.IsDeleted == false
                             && assetLease.Id == leaseId
                             select new
                             {
                                 Project = assetLease.Project.Name,
                                 Location = assetLease.Project.Location.Name,
                                 assetLease.LeaseNumber,
                                 ProjectDescription = assetLease.Project.Description,
                                 assetLease.Id,
                                 Updates = assetLease.AssetLeaseUpdates.Where(u => u.IsDeleted == false).Select(s => new
                                 {
                                     s.Id,
                                     s.UpdateDate
                                 }).OrderBy(d => d.UpdateDate).ToList()
                             }).FirstOrDefault();
                return lease;
            }
        }

        public static object GetLeaseUpdateEntries(string leaseUpdateId)
        {
            using(var dbContext = new AppDataContext())
            {
                var leaseUpdate = (from update in dbContext.AssetLeaseUpdates
                                   where update.Id == leaseUpdateId
                                   && update.IsDeleted == false
                                   select new
                                   {
                                       update.UpdateDate,
                                       update.Id,
                                       Entries = update.AssetLeaseUpdateEntries.Where(ntr => ntr.IsDeleted == false && ntr.AssetLeaseEntry.IsDeleted == false).Select(ntr => new
                                       {
                                           ntr.Id,
                                           ntr.Comment,
                                           ntr.AssetStatus,
                                           ntr.AssetLeaseEntryId,
                                           ntr.AssetLeaseEntry.LeaseCost,
                                           AssetItem = ntr.AssetLeaseEntry.AssetItem.Asset.Name,
                                           AssetBrand = ntr.AssetLeaseEntry.AssetItem.AssetBrand.Name,
                                           AssetGroup = ntr.AssetLeaseEntry.AssetItem.AssetGroup.Name,
                                           AssetModel = ntr.AssetLeaseEntry.AssetItem.AssetModel.Name,
                                           AssetSubGroup = ntr.AssetLeaseEntry.AssetItem.AssetSubGroup.Name,
                                           AssetType = ntr.AssetLeaseEntry.AssetItem.AssetType.Name,
                                           AssetCapacity = ntr.AssetLeaseEntry.AssetItem.Capacity.Name,
                                           AssetCode = ntr.AssetLeaseEntry.AssetItem.Code,
                                           AssetDimension = ntr.AssetLeaseEntry.AssetItem.Dimension.Name,
                                           AssetSerialNumber = ntr.AssetLeaseEntry.AssetItem.SerialNumber
                                       }).ToList()
                                   }).FirstOrDefault();
                return leaseUpdate;
            }
        }

        public static bool UpdateLeaseUpdateStatus(List<LeaseStatusUpdateRequest> updateRequests, string loggedInUser)
        {
            using(var dbContext = new AppDataContext())
            {
                bool success = false;
                var today = DateTime.Now;
                foreach(var entry in updateRequests)
                {
                    var affectedUpdateEntry = dbContext.AssetLeaseUpdateEntries.Include(s=>s.AssetLeaseEntry).FirstOrDefault(ntr => ntr.Id == entry.Id && ntr.IsDeleted == false);
                    if (affectedUpdateEntry != null)
                    {
                        var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(d => d.Id == affectedUpdateEntry.AssetLeaseEntry.AssetItemId);
                        if (affectedUpdateEntry.AssetStatus != AssetStatus.RETURNED || affectedAssetItem.IsAvailable)
                        {
                            affectedUpdateEntry.AssetStatus = entry.AssetStatus;
                            affectedUpdateEntry.LastModifiedById = loggedInUser;
                            affectedUpdateEntry.LastModifiedDate = today;

                            var lastUpdateEntry = dbContext.AssetLeaseUpdateEntries.Where(d => d.IsDeleted == false && d.UpdateDate.Date >= affectedUpdateEntry.UpdateDate.Date && d.AssetLeaseEntryId == affectedUpdateEntry.AssetLeaseEntryId).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
                            if (lastUpdateEntry.Id == affectedUpdateEntry.Id)
                            {
                                var affectedLeaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == affectedUpdateEntry.AssetLeaseEntryId);

                                affectedLeaseEntry.AssetCurrentStatus = entry.AssetStatus;
                                affectedAssetItem.CurrentStatus = entry.AssetStatus;
                            }
                            //save a history of the edits
                            success = true;
                        }

                    }
                }
                if (success)
                {
                    dbContext.SaveChanges();
                }
                
                return success;
            }
        }

        public static object GetLeaseEntriesForUpdate(string leaseId)
        {
            using(var dbConctext = new AppDataContext())
            {
                var leaseDetails = (from assetLease in dbConctext.AssetLeases
                                    where assetLease.IsDeleted == false && assetLease.Id == leaseId
                                    select new LeaseUpdateDetails
                                    {
                                        Project = assetLease.Project.Name,
                                        Location = assetLease.Project.Location.Name,
                                        Subsidiary = assetLease.Project.Subsidiary.Name,
                                        LeaseNumber = assetLease.LeaseNumber,
                                        AssetLeaseId = assetLease.Id,
                                        Entries = assetLease.AssetLeaseEntries.Where(s => s.IsDeleted == false /*&& s.AssetCurrentStatus != AssetStatus.RETURNED*/)
                                            .Select(ntr => new LeaseEntryUpdateRequest
                                            {
                                                AssetLeaseEntryId = ntr.Id,
                                                AssetGroup = ntr.AssetItem.AssetGroup.Name,
                                                AssetSubGroup = ntr.AssetItem.AssetSubGroup.Name,
                                                Description = ntr.AssetItem.Asset.Name,
                                                AssetCode = ntr.AssetItem.Code,
                                                AssetType = ntr.AssetItem.AssetType.Name,
                                                AssetBrand = ntr.AssetItem.AssetBrand.Name,
                                                AssetModel = ntr.AssetItem.AssetModel.Name,
                                                AssetCapacity = ntr.AssetItem.Capacity.Name,
                                                AssetSerialNumber = ntr.AssetItem.SerialNumber,
                                                EngineModel = ntr.AssetItem.EngineModel.Name,
                                                EngineSerialNumber = ntr.AssetItem.EngineNumber,
                                                ExpectedLeaseOutDate = ntr.ExpectedLeaseOutDate
                                            }).ToList()
                                    }).FirstOrDefault();
                return leaseDetails;
            }
        }

        public static bool CreateLeaseUpdate(LeaseUpdateRequest leaseUpdate, string loggedInUser)
        {
            using(var dbContext = new AppDataContext())
            {
                var today = DateTime.Now;
                var previouslyUpdated = dbContext.AssetLeaseUpdates.FirstOrDefault(d => d.AssetLeaseId == leaseUpdate.AssetLeaseId && d.IsDeleted == false && d.UpdateDate.Date == leaseUpdate.UpdateDate.Date);
                var lastLeaseUpdate = dbContext.AssetLeaseUpdates.Where(a => a.AssetLeaseId == leaseUpdate.AssetLeaseId && a.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
                bool shouldUpdateAssetLeaseCurrentStatus = (lastLeaseUpdate != null && lastLeaseUpdate.UpdateDate <= leaseUpdate.UpdateDate) || lastLeaseUpdate == null;
                //var affectedLease = dbContext.AssetLeases.FirstOrDefault(a=>a.Id == leaseUpdate.Id && a.IsDeleted==false)
                
                if (previouslyUpdated == null)
                {
                    var newUpdate = new AssetLeaseUpdate
                    {
                        AssetLeaseId = leaseUpdate.AssetLeaseId,
                        CreationDate = today,
                        IsDeleted = false,
                        CreatedById = loggedInUser,
                        UpdateDate = leaseUpdate.UpdateDate,
                        InvoiceRaised = false,
                        //LeaseInvoiceId = null
                    };

                    foreach (var entry in leaseUpdate.Entries)
                    {
                        var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == entry.AssetLeaseEntryId && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date <= leaseUpdate.UpdateDate.Date);
                        if (affectedleaseEntry != null)
                        {
                            var updateEntry = new AssetLeaseUpdateEntry
                            {
                                CreationDate = today,
                                IsDeleted = false,
                                AssetStatus = entry.FunctionalStatus,
                                DateDeleted = null,
                                DeletedById = null,
                                LastModifiedDate = today,
                                AssetLeaseUpdate = newUpdate,
                                Comment = entry.Remark,
                                UpdateDate = leaseUpdate.UpdateDate,
                                AssetLeaseEntryId = entry.AssetLeaseEntryId,
                                CreatedById = loggedInUser
                            };

                            dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

                            if (shouldUpdateAssetLeaseCurrentStatus)
                            {
                                affectedleaseEntry.AssetCurrentStatus = entry.FunctionalStatus;
                                var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(d => d.Id == affectedleaseEntry.AssetItemId);
                                affectedAssetItem.CurrentStatus = entry.FunctionalStatus;
                            }
                        }
                    }

                    dbContext.SaveChanges();
                }
                else
                {
                    foreach(var entry in leaseUpdate.Entries)
                    {
                        var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == entry.AssetLeaseEntryId && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date <= leaseUpdate.UpdateDate.Date);
                        if (affectedleaseEntry != null)
                        {
                            var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(d => d.Id == affectedleaseEntry.AssetItemId);
                            var affectedLeaseUpdateEntry = dbContext.AssetLeaseUpdateEntries.FirstOrDefault(d => d.AssetLeaseEntryId == entry.AssetLeaseEntryId && d.IsDeleted == false && d.AssetLeaseUpdateId == previouslyUpdated.Id);
                            if (affectedLeaseUpdateEntry != null && (affectedLeaseUpdateEntry.AssetStatus != AssetStatus.RETURNED || affectedAssetItem.IsAvailable))
                            {
                                affectedLeaseUpdateEntry.AssetStatus = entry.FunctionalStatus;
                                affectedLeaseUpdateEntry.LastModifiedById = loggedInUser;
                                affectedLeaseUpdateEntry.LastModifiedDate = today;
                                affectedLeaseUpdateEntry.Comment = entry.Remark;
                                if (shouldUpdateAssetLeaseCurrentStatus)
                                {
                                    affectedleaseEntry.AssetCurrentStatus = entry.FunctionalStatus;

                                    affectedAssetItem.CurrentStatus = entry.FunctionalStatus;
                                }
                            }
                        }
                    }
                    dbContext.SaveChanges();
                }
                return true;
            }
        }

        public static BatchUploadResponse AssetLeaseExcelUpdate(LeaseUpdateExcelUpload leaseUpdate, string loggedInUser)
        {
            using (var dbContext = new AppDataContext())
            {
                bool errorEncountered = false;
                var today = DateTime.Now;
                var previouslyUpdated = dbContext.AssetLeaseUpdates.FirstOrDefault(d => d.AssetLeaseId == leaseUpdate.AssetLeaseId && d.IsDeleted == false && d.UpdateDate.Date == leaseUpdate.UpdateDate.Date);
                var lastLeaseUpdate = dbContext.AssetLeaseUpdates.Where(a => a.AssetLeaseId == leaseUpdate.AssetLeaseId && a.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
                bool shouldUpdateAssetLeaseCurrentStatus = (lastLeaseUpdate != null && lastLeaseUpdate.UpdateDate <= leaseUpdate.UpdateDate) || lastLeaseUpdate == null;
                //var affectedLease = dbContext.AssetLeases.FirstOrDefault(a=>a.Id == leaseUpdate.Id && a.IsDeleted==false)

                if (previouslyUpdated == null)
                {
                    var newUpdate = new AssetLeaseUpdate
                    {
                        AssetLeaseId = leaseUpdate.AssetLeaseId,
                        CreationDate = today,
                        IsDeleted = false,
                        CreatedById = loggedInUser,
                        UpdateDate = leaseUpdate.UpdateDate,
                        InvoiceRaised = false,
                        //LeaseInvoiceId = null
                    };

                    foreach (var entry in leaseUpdate.Entries)
                    {
                        entry.ErrorComment = "";
                        var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(s => s.Code == entry.AssetCode.Trim().ToLower() && s.IsDeleted == false);
                        if (affectedAssetItem != null)
                        {
                            if (AssetUpdateStatuses.TryGetValue(entry.FunctionalStatus.Trim(), out string foundStatus))
                            {
                                var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.AssetItemId == affectedAssetItem.Id && d.AssetLeaseId == leaseUpdate.AssetLeaseId && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date <= leaseUpdate.UpdateDate.Date);
                                if (affectedleaseEntry != null)
                                {
                                    var updateEntry = new AssetLeaseUpdateEntry
                                    {
                                        CreationDate = today,
                                        IsDeleted = false,
                                        AssetStatus = foundStatus,
                                        DateDeleted = null,
                                        DeletedById = null,
                                        LastModifiedDate = today,
                                        AssetLeaseUpdate = newUpdate,
                                        Comment = entry.Remark,
                                        UpdateDate = leaseUpdate.UpdateDate,
                                        AssetLeaseEntryId = affectedleaseEntry.Id,
                                        CreatedById = loggedInUser
                                    };

                                    dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

                                    if (shouldUpdateAssetLeaseCurrentStatus)
                                    {
                                        affectedleaseEntry.AssetCurrentStatus = entry.FunctionalStatus;
                                        affectedAssetItem.CurrentStatus = entry.FunctionalStatus;
                                    }
                                }
                            }
                            else
                            {
                                errorEncountered = true;
                                entry.ErrorComment += "Functional Status not valid";
                            }
                        }

                        else
                        {
                            errorEncountered = true;
                            entry.ErrorComment += "Asset Code not valid";
                        }
                    }
                }
                else
                {
                    foreach (var entry in leaseUpdate.Entries)
                    {
                        entry.ErrorComment = "";
                        var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(s => s.Code == entry.AssetCode.Trim().ToLower() && s.IsDeleted == false);
                        if (affectedAssetItem != null)
                        {
                            var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.AssetItemId == affectedAssetItem.Id && d.AssetLeaseId == leaseUpdate.AssetLeaseId && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date <= leaseUpdate.UpdateDate.Date);
                            if (affectedleaseEntry != null)
                            {
                                if (AssetUpdateStatuses.TryGetValue(entry.FunctionalStatus.Trim(), out string foundStatus))
                                {
                                    var affectedLeaseUpdateEntry = dbContext.AssetLeaseUpdateEntries.FirstOrDefault(d => d.AssetLeaseEntryId == affectedleaseEntry.Id && d.IsDeleted == false && d.AssetLeaseUpdateId == previouslyUpdated.Id);
                                    if (affectedLeaseUpdateEntry != null && (affectedLeaseUpdateEntry.AssetStatus!= AssetStatus.RETURNED || affectedAssetItem.IsAvailable))
                                    {
                                        affectedLeaseUpdateEntry.AssetStatus = foundStatus;
                                        affectedLeaseUpdateEntry.LastModifiedById = loggedInUser;
                                        affectedLeaseUpdateEntry.LastModifiedDate = today;
                                        affectedLeaseUpdateEntry.Comment = entry.Remark;

                                        if (shouldUpdateAssetLeaseCurrentStatus)
                                        {
                                            affectedleaseEntry.AssetCurrentStatus = entry.FunctionalStatus;
                                            affectedAssetItem.CurrentStatus = entry.FunctionalStatus;
                                        }
                                    }
                                }
                                else
                                {
                                    errorEncountered = true;
                                    entry.ErrorComment += "Functional Status not valid";
                                }
                            }
                        }
                        else
                        {
                            errorEncountered = true;
                            entry.ErrorComment += "Asset Code not valid";
                        }
                    }
                }
                if (!errorEncountered)
                {
                    dbContext.SaveChanges();
                }
                return new BatchUploadResponse
                {
                    ErrorList = errorEncountered ? leaseUpdate.Entries : null,
                    IsSuccessful = !errorEncountered
                };
            }
        }


        #region RoutineUpdateLogic
        //public static void RoutingUpdateCheck(string loggedInUser)
        //{
        //    using(var dbContext = new AppDataContext())
        //    {
        //        var pendingLeaseIds = dbContext.AssetLeases.Where(e => e.AssetLeaseEntries.Any(s => s.AssetCurrentStatus != AssetStatus.RETURNED) && e.IsDeleted == false)
        //            .Select(s =>s.Id).ToList();
        //        var today = DateTime.Now;

        //        foreach(var leaseId in pendingLeaseIds)
        //        {
        //            var leaseEntries = dbContext.AssetLeaseEntries.Where(s => s.AssetLeaseId == leaseId && s.IsDeleted == false).Include(ntr => ntr.AssetLeaseUpdateEntries).ToList();
        //            if (leaseEntries.Count > 0)
        //            {
        //                var exisitingUpdates = dbContext.AssetLeaseUpdates.Where(s => s.AssetLeaseId == leaseId && s.IsDeleted == false).Select(s => new { UpdateDate = s.UpdateDate.Date, s.Id }).ToList();
        //                var exisitingUpdateDates = exisitingUpdates.Select(s => s.UpdateDate);
        //                var leastLeaseOutDate = leaseEntries.OrderBy(d => d.ExpectedLeaseOutDate).Select(d => d.ExpectedLeaseOutDate).FirstOrDefault();

        //                var startDate = leastLeaseOutDate.Date;
        //                var endDate = DateTime.Now.Date;

        //                var allDates = Enumerable.Range(0, (endDate - startDate).Days)
        //                                         .Select(i => startDate.AddDays(i));

        //                var missingDates = allDates.Except(exisitingUpdateDates);

        //                foreach(var missingDate in missingDates)
        //                {
        //                    var assetUpdate = new AssetLeaseUpdate
        //                    {
        //                        AssetLeaseId = leaseId,
        //                        CreationDate = today,
        //                        IsDeleted = false,
        //                        CreatedById = loggedInUser,
        //                        UpdateDate = missingDate,
        //                        InvoiceRaised = false,
        //                        LeaseInvoiceId = null
        //                    };

        //                    foreach (var entry in leaseEntries)
        //                    {
        //                        //var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == entry.AssetLeaseEntryId && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date <= leaseUpdate.UpdateDate.Date);
        //                        if (entry.ExpectedLeaseOutDate.Date <= missingDate.Date)
        //                        {
        //                            var lastUpdateStatus = dbContext.AssetLeaseUpdateEntries.Where(s => s.AssetLeaseEntryId == entry.Id && s.IsDeleted == false && s.UpdateDate.Date <= missingDate.Date).OrderByDescending(d => d.UpdateDate).Select(s => s.AssetStatus).FirstOrDefault();
        //                            var lastUpdate = dbContext.AssetLeaseUpdateEntries.Where(d => d.AssetLeaseEntryId == entry.Id && d.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
        //                            var updateEntry = new AssetLeaseUpdateEntry
        //                            {
        //                                CreationDate = today,
        //                                IsDeleted = false,
        //                                AssetStatus = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
        //                                DateDeleted = null,
        //                                DeletedById = null,
        //                                LastModifiedDate = today,
        //                                AssetLeaseUpdate = assetUpdate,
        //                                Comment = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
        //                                UpdateDate = missingDate,
        //                                AssetLeaseEntryId = entry.Id,
        //                                CreatedById = loggedInUser
        //                            };

        //                            dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

        //                            if ((lastUpdate != null && lastUpdate.UpdateDate <= missingDate.Date) || lastUpdate == null)
        //                            {
        //                                entry.AssetCurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
        //                                var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(s => s.Id == entry.AssetItemId);

        //                                //also update the asset Item Status
        //                                affectedAssetItem.CurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
        //                            }
        //                        }
        //                    }
        //                }
        //                //do the update here;

        //                foreach(var existingUpdate in exisitingUpdates)
        //                {
        //                    var missingUpdateEntries = leaseEntries.Where(d => d.ExpectedLeaseOutDate.Date <= existingUpdate.UpdateDate.Date && !d.AssetLeaseUpdateEntries.Any(ntr => ntr.UpdateDate.Date == existingUpdate.UpdateDate.Date));
        //                    foreach(var missingEntry in missingUpdateEntries)
        //                    {
        //                        var lastUpdateStatus = dbContext.AssetLeaseUpdateEntries.Where(s => s.AssetLeaseEntryId == missingEntry.Id && s.IsDeleted == false && s.UpdateDate.Date<= existingUpdate.UpdateDate.Date).OrderByDescending(d => d.UpdateDate).Select(s =>  s.AssetStatus).FirstOrDefault();
        //                        var lastUpdate = dbContext.AssetLeaseUpdateEntries.Where(d => d.AssetLeaseEntryId == missingEntry.Id && d.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
        //                        var updateEntry = new AssetLeaseUpdateEntry
        //                        {
        //                            CreationDate = today,
        //                            IsDeleted = false,
        //                            AssetStatus = lastUpdateStatus?? AssetStatus.OPERATIONAL,
        //                            DateDeleted = null,
        //                            DeletedById = null,
        //                            LastModifiedDate = today,
        //                            AssetLeaseUpdateId = existingUpdate.Id,
        //                            Comment = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
        //                            UpdateDate = existingUpdate.UpdateDate,
        //                            AssetLeaseEntryId = missingEntry.Id,
        //                            CreatedById = loggedInUser
        //                        };

        //                        dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

        //                        if ((lastUpdate != null && lastUpdate.UpdateDate <= existingUpdate.UpdateDate.Date) || lastUpdate == null)
        //                        {
        //                            missingEntry.AssetCurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
        //                            var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(s => s.Id == missingEntry.AssetItemId);

        //                            //also update the asset Item Status
        //                            affectedAssetItem.CurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
        //                        }
        //                    }
        //                }

        //                //var missingUpdateEntries = dbContext.AssetLeaseUpdateEntries.Where(d => d.IsDeleted==false && leaseEntries.Any(ntr => ntr.Id == d.AssetLeaseEntryId ) && exisitingUpdateDates.Any(up => up.Date != d.UpdateDate.Date)).ToList();
        //            }
        //        }

        //        dbContext.SaveChanges();
        //    }
        //}
        #endregion
    }
}