using Application.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class BasicUserSeed
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            //Seed Default User
            ApplicationUser defaultUser = new ApplicationUser
            {
                UserName = "basicuser",
                Email = "basicuser@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            await userManager.CreateAsync(defaultUser, "123Pa$$word!");
            await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());
        }
    }
}
