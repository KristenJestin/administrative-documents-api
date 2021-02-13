using Application.Interfaces.Services;
using Infrastructure.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ICryptographyService, AesCryptographyService>();
        }
    }
}
