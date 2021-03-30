using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using equipment_lease_api.DTO;
using equipment_lease_api.Entities;
using static equipment_lease_api.Helpers.Constants.Strings;

namespace equipment_lease_api.Services
{
    public static class LeaseQuery
    {
        public static string CreateNewAssetLease(AssetLeaseDTO assetLeaseDTO, string loggedInUserId)
        {
            using (var dbContext = new AppDataContext())
            {
                var today = DateTime.Now;

                var newLease = new AssetLease
                {
                    DeletedById = null,
                    CreationDate = today,
                    IsDeleted = false,
                    CreatedById = loggedInUserId,
                    LeaseNumber = "LS",
                    ProjectId = assetLeaseDTO.ProjectId
                };

                foreach (var item in assetLeaseDTO.AssetList)
                {
                    var entry = new AssetLeaseEntry
                    {
                        DeletedById = null,
                        ActualReturnDate = null,
                        CreationDate = today,
                        ExpectedLeaseOutDate = item.ExpectedLeaseOutDate,
                        ExpectedReturnDate = item.ExpectedReturnDate,
                        IsDeleted = false,
                        AssetCurrentStatus = AssetStatus.OPERATIONAL,
                        AssetItemId = item.AssetId,
                        AssetLease = newLease,
                        CreatedById = loggedInUserId,
                        LeaseCost = item.LeaseCost,
                        ProjectSiteId = item.ProjectSiteId
                        //LocationId = item.LocationId,
                        //ProjectId = item.ProjectId
                    };

                    var affectedItem = dbContext.AssetItems.FirstOrDefault(d => d.Id == item.AssetId);
                    affectedItem.CurrentStatus = AssetStatus.LEASED_OUT;

                    dbContext.AssetLeaseEntries.Add(entry);
                }

                dbContext.AssetLeases.Add(newLease);
                dbContext.SaveChanges();
                //newLease.LeaseNumber = $"LS{today.Year}{today.Month}{today.Day}{newLease.NativeId:D6}";
                newLease.LeaseNumber = $"LS{newLease.NativeId:D6}";
                dbContext.SaveChanges();
                return newLease.LeaseNumber;
            }
        }
        public static object SearchForLeaseNumbers(string searchText)
        {
            using(var dbContext = new AppDataContext())
            {
                var leases = (from lease in dbContext.AssetLeases.Where(l => l.IsDeleted == false && l.LeaseNumber.ToLower().Contains(searchText.ToLower().Trim()))
                              select new
                              {
                                  lease.LeaseNumber,
                                  lease.Id,
                                  Date = lease.CreationDate,
                                  Location = lease.Project.Location.Name,
                                  Subsidiary = lease.Project.Subsidiary.Name,
                                  Project = lease.Project.Name,
                                  SubsidiaryId = lease.Project.SubsidiaryId,
                                  lease.ProjectId
                              }).ToList();
                return leases;
            }
        }
        public static AssetLeaseDTO GetAssetLeaseEntries(string id)
        {
            using (var dbContext = new AppDataContext())
            {
                var leaseDetails = (from lease in dbContext.AssetLeases
                                    where lease.IsDeleted == false
                                    && lease.Id == id
                                    //&& !lease.AssetLeaseEntries.Where(d => d.IsDeleted == false).Any(e => e.AssetCurrentStatus == AssetStatus.OPERATIONAL)
                                    select new AssetLeaseDTO
                                    {
                                        Id = lease.Id,
                                        ProjectId = lease.ProjectId,
                                        SubsidiaryId = lease.Project.SubsidiaryId,
                                        LeaseDate = lease.CreationDate,
                                        LeaseNumber = lease.LeaseNumber,
                                        AssetList = lease.AssetLeaseEntries.Where(item => item.IsDeleted == false && item.AssetCurrentStatus != AssetStatus.RETURNED).Select(item => new AssetLeaseItem
                                        {
                                            Id = item.Id,
                                            AssetId = item.AssetItemId,
                                            ExpectedLeaseOutDate = item.ExpectedLeaseOutDate,
                                            ExpectedReturnDate = item.ExpectedReturnDate,
                                            LeaseCost = item.LeaseCost,
                                            AssetBrand = item.AssetItem.AssetBrand.Name,
                                            AssetCode = item.AssetItem.Code,
                                            AssetName = item.AssetItem.Asset.Name,
                                            SerialNumber = item.AssetItem.SerialNumber,
                                            ProjectSiteId = item.ProjectSiteId,
                                            ProjectSite = item.ProjectSite.Name
                                        }).ToList()
                                    }).FirstOrDefault();
                return leaseDetails;
            }
        }
        public static bool UpdateAssetLease(AssetLeaseDTO assetLeaseDTO, string loggedInUserId)
        {
            using(var dbContext = new AppDataContext())
            {
                var affectedLease = dbContext.AssetLeases.FirstOrDefault(aa => aa.Id == assetLeaseDTO.Id && aa.IsDeleted == false);
                if (affectedLease != null)
                {
                    var today = DateTime.Now;
                    var updateStartDate = DateTime.Now.AddDays(-1);
                    affectedLease.ProjectId = assetLeaseDTO.ProjectId;
                    //create edit history in case they want to adopt and use the application

                    List<AssetLeaseUpdate> leaseUpdates = new List<AssetLeaseUpdate>();

                    //Get all current active assets
                    var existingAssets = dbContext.AssetLeaseEntries.Where(en => en.AssetLeaseId == affectedLease.Id && en.IsDeleted == false);

                    //Get all new assets coming from the client that does not exist previoulsy for the selected lease detail
                    var addedAssets = assetLeaseDTO.AssetList.Where(src => existingAssets.All(des => src.AssetId != des.AssetItemId));

                    //Get all asset coming from the client that already exist in the database for the selected lease
                    var updatedEntries = assetLeaseDTO.AssetList.Where(src => existingAssets.Any(des => src.AssetId == des.AssetItemId));

                    //Get all asset that are in the database but have been removed by the client
                    var removedEntries = existingAssets.ToList().Where(des => assetLeaseDTO.AssetList.All(src => src.AssetId != des.AssetItemId));

                    //remove each of such entries that have been removed by the client
                    foreach (var entry in removedEntries)
                    {
                        entry.IsDeleted = true;
                        entry.DeletedById = loggedInUserId;
                        entry.DateDeleted = today;

                        //var existingInvoiceEntries = dbContext.LeaseInvoiceEntries.Where(en => en.AssetLeaseEntryId == entry.Id);
                        var existingLeaseEntryUpdates = dbContext.AssetLeaseUpdateEntries.Where(an => entry.Id == an.AssetLeaseEntryId);
                        
                        //for each of the affected update entry for the selected asset, remove the update entry
                        foreach (var update in existingLeaseEntryUpdates)
                        {
                            update.IsDeleted = true;
                            update.DeletedById = loggedInUserId;
                            update.DateDeleted = today;
                        }
                    }

                    //for each of the organisms that have possibly been updated by the client, perform an update action on them
                    foreach (var entry in updatedEntries)
                    {
                        var affectedEntry = dbContext.AssetLeaseEntries.Where(rs => rs.Id == entry.Id && rs.IsDeleted == false).FirstOrDefault();
                        if (affectedEntry != null)
                        {
                            var previousLeaseOutDate = affectedEntry.ExpectedLeaseOutDate;
                            affectedEntry.ExpectedLeaseOutDate = entry.ExpectedLeaseOutDate;
                            affectedEntry.ExpectedReturnDate = entry.ExpectedReturnDate;
                            affectedEntry.LeaseCost = entry.LeaseCost;
                            affectedEntry.ProjectSiteId = entry.ProjectSiteId;

                            //if new leaseout date is greate
                            if (previousLeaseOutDate.Date > entry.ExpectedLeaseOutDate.Date && entry.ExpectedLeaseOutDate.Date < today.Date)
                            {
                                var numOfDaysDiff = (previousLeaseOutDate.Date - entry.ExpectedLeaseOutDate.Date).Days;
                                while (numOfDaysDiff > 0)
                                {
                                    var affectedAssetUpdate = dbContext.AssetLeaseUpdates.FirstOrDefault(a => a.AssetLeaseId == affectedLease.Id
                                        && a.UpdateDate.Date == entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff) && a.IsDeleted == false);

                                    if (affectedAssetUpdate == null)
                                    {
                                        affectedAssetUpdate = leaseUpdates.Where(s => s.AssetLeaseId == affectedLease.Id && s.UpdateDate.Date == entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff)).FirstOrDefault();
                                        if (affectedAssetUpdate == null)
                                        {
                                            affectedAssetUpdate = new AssetLeaseUpdate
                                            {
                                                AssetLeaseId = affectedLease.Id,
                                                DateDeleted = null,
                                                CreationDate = today,
                                                DeletedById = null,
                                                IsDeleted = false,
                                                UpdateDate = entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff),
                                                CreatedById = loggedInUserId,
                                                InvoiceRaised = false,
                                                LeaseInvoiceId = null
                                            };
                                            dbContext.AssetLeaseUpdates.Add(affectedAssetUpdate);
                                            leaseUpdates.Add(affectedAssetUpdate);
                                        }
                                    }

                                    if (!dbContext.AssetLeaseUpdateEntries.Any(aup => aup.AssetLeaseEntryId == affectedEntry.Id && aup.AssetLeaseUpdateId == affectedAssetUpdate.Id && aup.IsDeleted == false))
                                    {
                                        var lastUpdateStatus = dbContext.AssetLeaseUpdateEntries.Where(s => s.AssetLeaseEntry.AssetItemId == affectedEntry.AssetItemId && s.IsDeleted == false && s.UpdateDate < entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff)).OrderByDescending(d => d.UpdateDate).Select(s => s.AssetStatus).FirstOrDefault();
                                        var newUpdateEntry = new AssetLeaseUpdateEntry
                                        {
                                            DateDeleted = null,
                                            DeletedById = null,
                                            CreationDate = today,
                                            IsDeleted = false,
                                            UpdateDate = entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff),
                                            AssetLeaseEntry = affectedEntry,
                                            AssetLeaseUpdate = affectedAssetUpdate,
                                            AssetStatus = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
                                            Comment = "",
                                            CreatedById = loggedInUserId
                                        };
                                        dbContext.AssetLeaseUpdateEntries.Add(newUpdateEntry);
                                    }

                                    numOfDaysDiff--;
                                }
                            }

                            else if(entry.ExpectedLeaseOutDate.Date > previousLeaseOutDate.Date)
                            {
                                //how do we delete all updates if no asset update entry belongs to that date after the expected lease update are adjusted????
                                var affectedUpdates = dbContext.AssetLeaseUpdateEntries.Where(aup => aup.AssetLeaseEntryId == affectedEntry.Id && aup.IsDeleted == false && aup.UpdateDate.Date < entry.ExpectedLeaseOutDate.Date).ToList();
                                if (affectedUpdates.Count > 0)
                                {
                                    foreach(var upDateEntry in affectedUpdates)
                                    {
                                        upDateEntry.IsDeleted = true;
                                        upDateEntry.DeletedById = loggedInUserId;
                                    }
                                }
                            }
                        }
                    }

                    //for newly added asset
                    foreach (var item in addedAssets)
                    {
                        //var serrors = ModelState.Values.SelectMany(s => s.Errors);
                        var entry = new AssetLeaseEntry
                        {
                            DeletedById = null,
                            ActualReturnDate = null,
                            CreationDate = today,
                            ExpectedLeaseOutDate = item.ExpectedLeaseOutDate,
                            ExpectedReturnDate = item.ExpectedReturnDate,
                            IsDeleted = false,
                            AssetCurrentStatus = null,
                            AssetItemId = item.AssetId,
                            AssetLeaseId = affectedLease.Id,
                            CreatedById = loggedInUserId,
                            LeaseCost = item.LeaseCost,
                            ProjectSiteId = item.ProjectSiteId
                        };

                        var assetLeaseEntry = dbContext.AssetLeaseEntries.Add(entry).Entity;

                        if(updateStartDate.Date > entry.ExpectedLeaseOutDate.Date)
                        {
                            var numOfDaysDiff = (updateStartDate.Date - entry.ExpectedLeaseOutDate.Date).Days;
                            while (numOfDaysDiff > 0)
                            {
                                var affectedAssetUpdate = dbContext.AssetLeaseUpdates.FirstOrDefault(a => a.AssetLeaseId == affectedLease.Id
                                    && a.UpdateDate.Date == entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff) && a.IsDeleted == false);
                                if (affectedAssetUpdate == null)
                                {
                                    affectedAssetUpdate = leaseUpdates.Where(s => s.AssetLeaseId == affectedLease.Id && s.UpdateDate.Date == entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff)).FirstOrDefault();
                                    if (affectedAssetUpdate == null)
                                    {
                                        affectedAssetUpdate = new AssetLeaseUpdate
                                        {
                                            AssetLeaseId = affectedLease.Id,
                                            DateDeleted = null,
                                            CreationDate = today,
                                            DeletedById = null,
                                            IsDeleted = false,
                                            UpdateDate = entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff),
                                            CreatedById = loggedInUserId,
                                            InvoiceRaised = false,
                                            LeaseInvoiceId = null
                                        };
                                        dbContext.AssetLeaseUpdates.Add(affectedAssetUpdate);
                                        leaseUpdates.Add(affectedAssetUpdate);
                                    }
                                }

                                var newUpdateEntry = new AssetLeaseUpdateEntry
                                {
                                    DateDeleted = null,
                                    DeletedById = null,
                                    CreationDate = today,
                                    IsDeleted = false,
                                    UpdateDate = entry.ExpectedLeaseOutDate.Date.AddDays(numOfDaysDiff),
                                    AssetLeaseEntry = assetLeaseEntry,
                                    AssetLeaseUpdate = affectedAssetUpdate,
                                    AssetStatus = AssetStatus.OPERATIONAL,
                                    Comment = "",
                                    CreatedById = loggedInUserId
                                };
                                dbContext.AssetLeaseUpdateEntries.Add(newUpdateEntry);

                                numOfDaysDiff--;
                            }
                        }
                    }

                    dbContext.SaveChanges();
                    return true;
                }
                return false;
            }
        }
    }
}
