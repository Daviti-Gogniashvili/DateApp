using System;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<ITokenService, TokenService>();
            services.AddControllersWithViews()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.WriteIndented = true;
                });
            services.AddDbContext<DataContext>(context => {
                context.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            return services;
        }
    }
}
