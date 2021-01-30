using System;
using System.Threading.Tasks;
using Application.Interfaces.Services;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Seeds;
using Infrastructure.Identity.Services;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi.Extensions
{
	public static class WebHostExtensions
	{
		public async static Task<IHost> SeedDataAsync(this IHost host)
		{
			using (IServiceScope scope = host.Services.CreateScope())
			{
				IServiceProvider services = scope.ServiceProvider;

				// context
				IdentityDbContext identityContext = services.GetRequiredService<IdentityDbContext>();
				ApplicationDbContext applicationDbContext = services.GetRequiredService<ApplicationDbContext>();

				// services
				IAccountService accountService = services.GetRequiredService<IAccountService>();

				// seeds				
				#region user
#if DEBUG
				if (!await identityContext.Accounts.AnyAsync())
				{
					await AdminUserSeed.SeedAsync(accountService, identityContext);
					await BasicUserSeed.SeedAsync(accountService, identityContext);
				}
#endif
				#endregion
				await identityContext.SaveChangesAsync();

				#region document
#if DEBUG
				if (!await applicationDbContext.DocumentTypes.AnyAsync())
					await DocumentTypeSeed.SeedAsync(applicationDbContext);
#endif
				#endregion
				await applicationDbContext.SaveChangesAsync();
			}

			return host;
		}
	}
}
