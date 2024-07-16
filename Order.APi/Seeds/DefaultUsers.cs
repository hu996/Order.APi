using Microsoft.AspNetCore.Identity;
using Order.APi.Constants;
using Order.Repository.Entities;

namespace Order.APi.Seeds
{
    public class DefaultUsers
    {

        public static async Task SeedSuperAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new AppUser
            {
                DisplayName = "SuperAdmin",
                UserName = "superadmin@gmail.com",
                Email = "superadmin@gmail.com",
                
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, Roles.SuperAdmin.ToString());
            }
           
        }


        public static async Task SeedAdminUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var defaultUser = new AppUser()
            {
                DisplayName = "Admin",
                UserName = "Admin@gmail.com",
                Email = "Admin@gmail.com",
              
            };

            var user = await userManager.FindByEmailAsync(defaultUser.Email);
            if (user == null)
            {
                await userManager.CreateAsync(defaultUser, "123Pa$$word!");
                await userManager.AddToRoleAsync(defaultUser, Roles.Admin.ToString());
            }
            
        }

    }
}
