using Application.DTOs.Account;
using Application.Interfaces.Services;
using Domain.Entities;
using Infrastructure.Identity.Contexts;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Identity.Seeds
{
    public static class BasicUserSeed
    {
        public static async Task SeedAsync(IAccountService service, IdentityDbContext context)
        {
            //Seed Default User
            string password = "123Pa$$word!";
            RegisterRequest register = new RegisterRequest
            {
                Username = "basicuser",
                Email = "basicuser@gmail.com",
                Password = password,
                ConfirmPassword = password                
            };

            Account account = await service.RegisterAsync(register);

            account.Verified = DateTime.UtcNow;
            context.Update(account);
            await context.SaveChangesAsync();
        }
    }
}
