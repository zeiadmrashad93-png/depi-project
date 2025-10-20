using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using befit.application.Contracts;
using befit.application.Services;
using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace befit.application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfigurationManager configuration)
        {
            var section = configuration.GetSection("Cloudinary");
            services.AddSingleton<ICloudinary, Cloudinary>(sp => new Cloudinary(
                new Account(section["Cloud"], section["ApiKey"], section["ApiSecret"])));

            services.AddScoped<IFileService, IFileService>();

            services.AddScoped<IMenuItemsService, MenuItemsService>();

            services.AddScoped<ICategoryService, CategoryService>();

            return services;
        }
    }
}
