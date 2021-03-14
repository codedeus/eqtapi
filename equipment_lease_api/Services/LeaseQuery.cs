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
                        AssetCurrentStatus = null,
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
                newLease.LeaseNumber = $"LS{today.Year}{today.Month}{today.Day}{newLease.NativeId:D6}";
                dbContext.SaveChanges();
                return newLease.LeaseNumber;
            }
        }
        public static object SearchForLeaseNumbers(string searchText)
        {
            using(var dbContext = new AppDataContext())
            {
                var leases = (from lease in dbContext.AssetLeases.Where(l => l.IsDeleted == false && l.LeaseNumber.ToLower().Contains(searchText.ToLower().Trim()))
                              select new { lease.LeaseNumber, lease.Id }).ToList();
                return leases;
            }
        }
        public static AssetLeaseDTO GetAssetsForEdit(string id)
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
                                        AssetList = lease.AssetLeaseEntries.Where(item => item.IsDeleted == false && item.AssetCurrentStatus != AssetStatus.RETURNED).Select(item => new AssetLeaseItem
                                        {
                                            Id = item.Id,
                                            AssetId = item.AssetItemId,
                                            ExpectedLeaseOutDate = item.ExpectedLeaseOutDate,
                                            ExpectedReturnDate = item.ExpectedReturnDate,
                                            LeaseCost = item.LeaseCost,
                                            AssetGroup = item.AssetItem.AssetGroup.Name,
                                            AssetGroupId = item.AssetItem.AssetGroupId,
                                            Brand = item.AssetItem.AssetBrand.Name,
                                            Code = item.AssetItem.Code,
                                            Name = item.AssetItem.Asset.Name,
                                            Number = item.AssetItem.SerialNumber,
                                            ProjectSiteId = item.ProjectSiteId
                                        }).ToList()
                                    }).FirstOrDefault();
                return leaseDetails;
            }
        }
    }
}
