using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using equipment_lease_api.Entities;
using equipment_lease_api.Models;
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
                    var affectedUpdateEntry = dbContext.AssetLeaseUpdateEntries.FirstOrDefault(ntr => ntr.Id == entry.Id && ntr.IsDeleted == false);
                    if (affectedUpdateEntry != null)
                    {
                        affectedUpdateEntry.AssetStatus = entry.AssetStatus;
                        affectedUpdateEntry.LastModifiedById = loggedInUser;
                        affectedUpdateEntry.LastModifiedDate = today;
                        //save a history of the edits
                        success = true;
                    }
                }
                dbContext.SaveChanges();
                return success;
            }
        }

        public static object GetLeaseEntriesForUpdate(string leaseId)
        {
            using(var dbConctext = new AppDataContext())
            {
                var leaseDetails = (from assetLease in dbConctext.AssetLeases
                                    where assetLease.IsDeleted == false && assetLease.Id == leaseId
                                    select new
                                    {
                                        Project = assetLease.Project.Name,
                                        Location = assetLease.Project.Location.Name,
                                        Subsidiary = assetLease.Project.Subsidiary.Name,
                                        LeaseNumber = assetLease.LeaseNumber,
                                        assetLease.Id,
                                        Entries = assetLease.AssetLeaseEntries.Where(s => s.IsDeleted == false && s.AssetCurrentStatus != AssetStatus.RETURNED)
                                            .Select(ntr => new LeaseEntryUpdateRequest
                                            {
                                                Id = ntr.Id,
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
                                                EngineSerialNumber = ntr.AssetItem.EngineNumber
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
                var previouslyUpdated = dbContext.AssetLeaseUpdates.FirstOrDefault(d => d.AssetLeaseId == leaseUpdate.Id && d.IsDeleted == false && d.UpdateDate.Date == leaseUpdate.UpdateDate.Date);
                var lastLeaseUpdate = dbContext.AssetLeaseUpdates.Where(a => a.AssetLeaseId == leaseUpdate.Id && a.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
                bool shouldUpdateAssetLeaseCurrentStatus = (lastLeaseUpdate != null && lastLeaseUpdate.UpdateDate <= leaseUpdate.UpdateDate) || lastLeaseUpdate == null;
                if (previouslyUpdated == null)
                {
                    var newUpdate = new AssetLeaseUpdate
                    {
                        AssetLeaseId = leaseUpdate.Id,
                        CreationDate = today,
                        IsDeleted = false,
                        CreatedById = loggedInUser,
                        UpdateDate = leaseUpdate.UpdateDate,
                        InvoiceRaised = false,
                        LeaseInvoiceId = null
                    };

                    foreach (var entry in leaseUpdate.Entries)
                    {
                        var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == entry.Id && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date >= leaseUpdate.UpdateDate.Date);
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
                                AssetLeaseEntryId = entry.Id,
                                CreatedById = loggedInUser
                            };

                            dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

                            if (shouldUpdateAssetLeaseCurrentStatus)
                            {
                                affectedleaseEntry.AssetCurrentStatus = entry.FunctionalStatus;
                            }
                        }
                    }

                    dbContext.SaveChanges();
                }
                else
                {
                    foreach(var entry in leaseUpdate.Entries)
                    {
                        var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == entry.Id && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date >= leaseUpdate.UpdateDate.Date);
                        if (affectedleaseEntry != null)
                        {
                            var affectedLeaseUpdateEntry = dbContext.AssetLeaseUpdateEntries.FirstOrDefault(d => d.AssetLeaseEntryId == entry.Id && d.IsDeleted == false && d.AssetLeaseUpdateId == previouslyUpdated.Id);
                            if (affectedLeaseUpdateEntry != null)
                            {
                                affectedLeaseUpdateEntry.AssetStatus = entry.FunctionalStatus;
                                affectedLeaseUpdateEntry.LastModifiedById = loggedInUser;
                                affectedLeaseUpdateEntry.LastModifiedDate = today;

                                if (shouldUpdateAssetLeaseCurrentStatus)
                                {
                                    affectedleaseEntry.AssetCurrentStatus = entry.FunctionalStatus;
                                }
                            }
                        }
                    }
                    dbContext.SaveChanges();
                }
                return true;
            }
        }
    }
}