using AutoWrapper;
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
using Application;
using Infrastructure.Shared;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Rewrite;
using WebApi.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

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
			services.AddCors();

			services.AddApplicationLayer();
			services.AddIdentityInfrastructure(Configuration);
			services.AddPersistenceInfrastructure(Configuration);
			services.AddSharedInfrastructure();

			services
				.AddControllers(options => options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())))
				.AddNewtonsoftJson();
			services.AddApiVersioningExtension();
			services.AddValidationExtension();
			// allow to manage model state via AutoWrapper
			services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

			services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
			{
				UseCustomSchema = true,
				UseApiProblemDetailsException = true,
				IgnoreNullValue = false
			});

			if (env.IsDevelopment())
			{
				//app.UseDeveloperExceptionPage();
			}

			app.UseRewriter(new RewriteOptions().Add(new KebabCaseRule()));
			app.UseRouting();

			// global cors policy
			app.UseCors(options => options
				.SetIsOriginAllowed(origin => true)
				.AllowAnyMethod()
				.AllowAnyHeader()
				.AllowCredentials());

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(options => options.MapControllers());
		}
	}
}
