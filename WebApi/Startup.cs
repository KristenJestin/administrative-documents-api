using AutoWrapper;
using Application.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddIdentityInfrastructure(Configuration);
			services.AddPersistenceInfrastructure(Configuration);

			services.AddControllers()
				.AddNewtonsoftJson();
			// allow to manage model state via AutoWrapper
			services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { UseApiProblemDetailsException = true });

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
