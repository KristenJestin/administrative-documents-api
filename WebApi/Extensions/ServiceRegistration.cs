using System;
using System.Linq;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Behaviours;

namespace WebApi.Extensions
{
	public static class ServiceRegistration
	{
		public static void AddValidationExtension(this IServiceCollection services)
		{
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
		}

		public static void AddApiVersioningExtension(this IServiceCollection services)
		{
			services.AddApiVersioning(config =>
			{
				config.DefaultApiVersion = new ApiVersion(1, 0);
				config.AssumeDefaultVersionWhenUnspecified = true;
				// Advertise the API versions supported for the particular endpoint
				config.ReportApiVersions = true;
			});
		}
	}
}
