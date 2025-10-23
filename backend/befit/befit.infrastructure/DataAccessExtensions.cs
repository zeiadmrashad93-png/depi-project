using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using befit.infrastructure.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using befit.infrastructure.Authentication;
using befit.domain.Contracts;

namespace befit.infrastructure
{
    public static class DataAccessExtensions
    {
       public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfigurationManager configurationManager)
       {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("befitDB"));
            });

            services.AddIdentityCore<AuthenticationUser>(options =>
            {
                options.User = new UserOptions
                {
                    RequireUniqueEmail = true
                };
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IAuthenticationManager, AuthenticationManager>();

            IServiceCollection serviceCollection = services.AddScoped<IUnitOfWork, UnitOfWork>();

            var jwtSettings = configurationManager.GetSection("Jwt").Get<JwtSettings>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer =jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            return services;
        }
    }
}
