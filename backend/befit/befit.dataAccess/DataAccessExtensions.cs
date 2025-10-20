using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.core.Contracts;
using befit.dataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using befit.core.Contracts;

namespace befit.dataAccess
{
    public static class DataAccessExtensions
    {
       public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfigurationManager configurationManager)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configurationManager.GetConnectionString("befitDB"));
            });

            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
