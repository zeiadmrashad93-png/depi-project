using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;
using befit.application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace befit.application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IFileService, IFileService>();

            services.AddScoped<IMenuItemsService, MenuItemsService>();

            return services;
        }
    }
}
