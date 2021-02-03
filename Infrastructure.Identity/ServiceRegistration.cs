using Application.Interfaces.Services;
using Domain.Settings;
using Infrastructure.Identity.Contexts;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Infrastructure.Identity
{
    public static class ServiceRegistration
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // database
            services.AddDbContext<IdentityDbContext>(options =>
                options.UseMySql(
                    configuration.GetConnectionString("IdentityConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("IdentityConnection"))
                )
            );

            #region services
            services.AddTransient<IAccountService, AccountService>();
            #endregion

            #region settings
            IConfigurationSection jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JwtSettings>(jwtSettingsSection);
            #endregion

            JwtSettings wwtSettings = jwtSettingsSection.Get<JwtSettings>();
            var key = Encoding.ASCII.GetBytes(wwtSettings.Key);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                        ClockSkew = TimeSpan.Zero
                    };
                }
            );

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //    .AddJwtBearer(o =>
            //    {
            //        o.RequireHttpsMetadata = false;
            //        o.SaveToken = false;
            //        o.TokenValidationParameters = new TokenValidationParameters
            //        {
            //            ValidateIssuerSigningKey = true,
            //            ValidateIssuer = true,
            //            ValidateAudience = true,
            //            ValidateLifetime = true,
            //            ClockSkew = TimeSpan.Zero,
            //            ValidIssuer = configuration["JWTSettings:Issuer"],
            //            ValidAudience = configuration["JWTSettings:Audience"],
            //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:Key"]))
            //        };

            //        o.Events = new JwtBearerEvents()
            //        {
            //            OnAuthenticationFailed = context
            //                // TODO: review the management of expired tokens that fall here
            //                => throw new ApiProblemDetailsException("An error occurred while processing your authentication key", (int)HttpStatusCode.BadRequest),
            //            OnChallenge = context
            //                => throw new ApiProblemDetailsException("You are not Authorized", (int)HttpStatusCode.Unauthorized),
            //            OnForbidden = context
            //                => throw new ApiProblemDetailsException("You are not authorized to access this resource", (int)HttpStatusCode.Forbidden),
            //        };
            //    });
        }
    }
}
