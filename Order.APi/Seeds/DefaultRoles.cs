using Microsoft.AspNetCore.Identity;
using Order.APi.Constants;
using System.Data;

namespace Order.APi.Seeds
{
    public class DefaultRoles
    {

        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any(r => r.Name == Roles.SuperAdmin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.SuperAdmin.ToString()));

            if (!roleManager.Roles.Any(r => r.Name == Roles.Admin.ToString()))
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));

           
        }
    }
}
