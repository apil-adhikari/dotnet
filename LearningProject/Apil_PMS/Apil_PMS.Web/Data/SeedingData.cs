using Apil_PMS.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace Apil_PMS.Web.Data
{
    public class SeedingData
    {
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            // adding custom roles:
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();


            string[] RoleNames = { "SUPERADMIN", "ADMIN", "USER" };

            foreach (var RoleName in RoleNames)
            {
                var RoleExist = await RoleManager.RoleExistsAsync(RoleName);
                if (!RoleExist)
                {
                    await RoleManager.CreateAsync(new IdentityRole(RoleName));
                }
            }

            //We can create a super user who will maintain the web app
            if (await UserManager.FindByEmailAsync("superadmin@pms.com") == null)
            {
                var Role = RoleManager.FindByNameAsync("SUPERADMIN").Result;
                var SuperAdminUser = new ApplicationUser()
                {
                    FirstName = "Super",
                    LastName = "Admin",
                    Address = "Earth",
                    Email = "superadmin@pms.com",
                    UserName = "superadmin@pms.com",
                    UserRoleId = Role.Id,
                    PhoneNumber = "1234567890",
                    CreatedBy = "SUPERADMIN",
                    CreatedDate = DateTime.Now
                };

                var result = await UserManager.CreateAsync(SuperAdminUser, "SuperAdmin@123");
                await UserManager.SetLockoutEnabledAsync(SuperAdminUser, false);

                // Assign the "SUPERADMIN" role to the super admin user
                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(SuperAdminUser, "SUPERADMIN");
                }
            }
        }
    }
}