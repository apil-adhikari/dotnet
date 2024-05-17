using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IMS.web.Data
{
    public class SeedingData
    {

       

        // static does not return anything but executes the code
        public static async Task InitilizeAsync(IServiceProvider serviceProvider)
        {
            //Start of Defining Role Manager-----
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            //End of Defining Role Manager------

            // Defining Roles
            string[] Roles = { "SUPERADMIN", "ADMIN", "COUNTER", "STORE", "PUBLIC" };

            foreach(string roleName in Roles)
            {
                if(!await _roleManager.RoleExistsAsync(roleName))
                {  
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
