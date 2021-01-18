using System;
using System.Threading.Tasks;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Models;
using Infrastructure.Identity.Seeds;
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
				IdentityContext identityContext = services.GetRequiredService<IdentityContext>();
				ApplicationDbContext applicationDbContext = services.GetRequiredService<ApplicationDbContext>();

				// user
				UserManager<ApplicationUser> userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
				RoleManager<ApplicationRole> roleManager = services.GetRequiredService<RoleManager<ApplicationRole>>();

				// seeds				
				#region user
				if (!await identityContext.Roles.AnyAsync())
					await RolesSeed.SeedAsync(userManager, roleManager);
#if DEBUG
				if (!await identityContext.Users.AnyAsync())
				{
					await SuperAdminSeed.SeedAsync(userManager, roleManager);
					await BasicUserSeed.SeedAsync(userManager, roleManager);
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
