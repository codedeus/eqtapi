using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                if (!dbContext.Departments.Any())
                {
                    Department admin = new Department { Name = "Admin" };

                    dbContext.Add(admin);
                    dbContext.SaveChanges();
                    
                    AppUser adminUser = new AppUser
                    {
                        UserName = "kanayochukwu@mail.com",
                        Email = "kanayochukwu@mail.com",
                        FirstName = "Kanayochukwu",
                        LastName = "Anidi",
                        DepartmentId = admin.Id,
                        Role = "Admin"
                    };

                    IdentityResult adminresult = userManager.CreateAsync(adminUser, "password").Result;

                    if (adminresult.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }
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
        }
    }
}
