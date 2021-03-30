using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static equipment_lease_api.Helpers.Constants.Strings;

namespace equipment_lease_api.Entities
{
    public class DataInitializer
    {
        public static class UserAndRoleDataInitializer
        {
            public static void SeedData(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, AppDataContext dbContext)
            {
                SeedRoles(roleManager);
                SeedUsers(userManager, dbContext);
            }

            private static void SeedUsers(UserManager<AppUser> userManager, AppDataContext dbContext)
            {
                var adminUser = userManager.FindByEmailAsync("kanayochukwu@mail.com").Result;
                
                var adminDepartment = dbContext.Departments.FirstOrDefault(d => d.Name == "Admin");

                if (adminDepartment == null)
                {
                    adminDepartment = new Department { Name = "Admin" };
                }

                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        UserName = "kanayochukwu@mail.com",
                        Email = "kanayochukwu@mail.com",
                        FirstName = "Kanayochukwu",
                        LastName = "Anidi",
                        Department = adminDepartment,
                        Role = "Admin"
                    };

                    IdentityResult adminresult = userManager.CreateAsync(adminUser, "password").Result;

                    if (adminresult.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }

                RoutingUpdateCheck(adminUser.Id);
            }

            private static void SeedRoles(RoleManager<IdentityRole> roleManager)
            {
                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    IdentityRole role = new IdentityRole
                    {
                        Name = "Admin"
                    };
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }
            }

           static void RoutingUpdateCheck(string loggedInUser)
            {
                using (var dbContext = new AppDataContext())
                {
                    var pendingLeaseIds = dbContext.AssetLeases.Where(e => e.AssetLeaseEntries.Any(s => s.AssetCurrentStatus != AssetStatus.RETURNED) && e.IsDeleted == false)
                        .Select(s => s.Id).ToList();
                    var today = DateTime.Now;

                    foreach (var leaseId in pendingLeaseIds)
                    {
                        var leaseEntries = dbContext.AssetLeaseEntries.Where(s => s.AssetLeaseId == leaseId && s.IsDeleted == false && s.AssetCurrentStatus != AssetStatus.RETURNED).Include(ntr => ntr.AssetLeaseUpdateEntries).ToList();
                        if (leaseEntries.Count > 0)
                        {
                            var exisitingUpdates = dbContext.AssetLeaseUpdates.Where(s => s.AssetLeaseId == leaseId && s.IsDeleted == false).Select(s => new { UpdateDate = s.UpdateDate.Date, s.Id }).ToList();
                            var exisitingUpdateDates = exisitingUpdates.Select(s => s.UpdateDate);
                            var leastLeaseOutDate = leaseEntries.OrderBy(d => d.ExpectedLeaseOutDate).Select(d => d.ExpectedLeaseOutDate).FirstOrDefault();

                            var startDate = leastLeaseOutDate.Date;
                            var endDate = DateTime.Now.Date;

                            var allDates = Enumerable.Range(0, (endDate - startDate).Days)
                                                     .Select(i => startDate.AddDays(i));

                            var missingDates = allDates.Except(exisitingUpdateDates);

                            foreach (var missingDate in missingDates)
                            {
                                var assetUpdate = new AssetLeaseUpdate
                                {
                                    AssetLeaseId = leaseId,
                                    CreationDate = today,
                                    IsDeleted = false,
                                    CreatedById = loggedInUser,
                                    UpdateDate = missingDate,
                                    InvoiceRaised = false,
                                    LeaseInvoiceId = null
                                };

                                foreach (var entry in leaseEntries)
                                {
                                    //var affectedleaseEntry = dbContext.AssetLeaseEntries.FirstOrDefault(d => d.Id == entry.AssetLeaseEntryId && d.IsDeleted == false && d.ExpectedLeaseOutDate.Date <= leaseUpdate.UpdateDate.Date);
                                    if (entry.ExpectedLeaseOutDate.Date <= missingDate.Date)
                                    {
                                        var lastUpdateStatus = dbContext.AssetLeaseUpdateEntries.Where(s => s.AssetLeaseEntryId == entry.Id && s.IsDeleted == false && s.UpdateDate.Date <= missingDate.Date).OrderByDescending(d => d.UpdateDate).Select(s => s.AssetStatus).FirstOrDefault();
                                        var lastUpdate = dbContext.AssetLeaseUpdateEntries.Where(d => d.AssetLeaseEntryId == entry.Id && d.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
                                        var updateEntry = new AssetLeaseUpdateEntry
                                        {
                                            CreationDate = today,
                                            IsDeleted = false,
                                            AssetStatus = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
                                            DateDeleted = null,
                                            DeletedById = null,
                                            LastModifiedDate = today,
                                            AssetLeaseUpdate = assetUpdate,
                                            Comment = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
                                            UpdateDate = missingDate,
                                            AssetLeaseEntryId = entry.Id,
                                            CreatedById = loggedInUser
                                        };

                                        dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

                                        if ((lastUpdate != null && lastUpdate.UpdateDate <= missingDate.Date) || lastUpdate == null)
                                        {
                                            entry.AssetCurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
                                            var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(s => s.Id == entry.AssetItemId);

                                            //also update the asset Item Status
                                            affectedAssetItem.CurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
                                        }
                                    }
                                }
                            }
                            //do the update here;

                            foreach (var existingUpdate in exisitingUpdates)
                            {
                                var missingUpdateEntries = leaseEntries.Where(d => d.ExpectedLeaseOutDate.Date <= existingUpdate.UpdateDate.Date && !d.AssetLeaseUpdateEntries.Any(ntr => ntr.UpdateDate.Date == existingUpdate.UpdateDate.Date));
                                foreach (var missingEntry in missingUpdateEntries)
                                {
                                    var lastUpdateStatus = dbContext.AssetLeaseUpdateEntries.Where(s => s.AssetLeaseEntryId == missingEntry.Id && s.IsDeleted == false && s.UpdateDate.Date <= existingUpdate.UpdateDate.Date).OrderByDescending(d => d.UpdateDate).Select(s => s.AssetStatus).FirstOrDefault();
                                    var lastUpdate = dbContext.AssetLeaseUpdateEntries.Where(d => d.AssetLeaseEntryId == missingEntry.Id && d.IsDeleted == false).OrderByDescending(d => d.UpdateDate).FirstOrDefault();
                                    var updateEntry = new AssetLeaseUpdateEntry
                                    {
                                        CreationDate = today,
                                        IsDeleted = false,
                                        AssetStatus = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
                                        DateDeleted = null,
                                        DeletedById = null,
                                        LastModifiedDate = today,
                                        AssetLeaseUpdateId = existingUpdate.Id,
                                        Comment = lastUpdateStatus ?? AssetStatus.OPERATIONAL,
                                        UpdateDate = existingUpdate.UpdateDate,
                                        AssetLeaseEntryId = missingEntry.Id,
                                        CreatedById = loggedInUser
                                    };

                                    dbContext.AssetLeaseUpdateEntries.Add(updateEntry);

                                    if ((lastUpdate != null && lastUpdate.UpdateDate <= existingUpdate.UpdateDate.Date) || lastUpdate == null)
                                    {
                                        missingEntry.AssetCurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
                                        var affectedAssetItem = dbContext.AssetItems.FirstOrDefault(s => s.Id == missingEntry.AssetItemId);

                                        //also update the asset Item Status
                                        affectedAssetItem.CurrentStatus = lastUpdate != null && lastUpdate.AssetStatus != null ? lastUpdate.AssetStatus : lastUpdateStatus ?? AssetStatus.OPERATIONAL;
                                    }
                                }
                            }

                            //var missingUpdateEntries = dbContext.AssetLeaseUpdateEntries.Where(d => d.IsDeleted==false && leaseEntries.Any(ntr => ntr.Id == d.AssetLeaseEntryId ) && exisitingUpdateDates.Any(up => up.Date != d.UpdateDate.Date)).ToList();
                        }
                    }

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
