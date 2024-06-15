using ApilSMS.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace ApilSMS.Web.Data
{
    public class SeedingData
    {
        // Method to seed roles and initilize admin user
        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            // Get the RoleManager and UserManager from the service provider
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            // Defining the roles we need for our applicaiton
            string[] roleNames = { "SUPERADMIN", "ADMIN", "STUDENT" };

            // Seeding Roles if they do not exist
            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // Seeding admin user if role doesn't exist
            if (await userManager.FindByEmailAsync("superadmin@sms.com") == null)
            {

                var role = roleManager.FindByNameAsync("SUPERADMIN").Result;
                var superAdminUser = new ApplicationUser
                {
                    Email = "superadmin@sms.com",
                    UserName = "superadmin@sms.com",
                    FirstName = "Super",
                    LastName = "Admin",
                    UserRoleId = role.Id,
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedDate = DateTime.Now
                };

                // Creating the super admin with password
                var result = await userManager.CreateAsync(superAdminUser, "SuperAdmin@123");
                await userManager.SetLockoutEnabledAsync(superAdminUser, false);

                // Assign the "SUPERADMIN" role to the super admin user
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(superAdminUser, "SUPERADMIN");
                }
            }
        }
    }
}
