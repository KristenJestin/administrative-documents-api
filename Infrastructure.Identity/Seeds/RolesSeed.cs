using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Application.Enums;

namespace Infrastructure.Identity.Seeds
{
    public static class RolesSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new ApplicationRole(Roles.SuperAdmin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new ApplicationRole(Roles.Basic.ToString()));
        }
    }
}
