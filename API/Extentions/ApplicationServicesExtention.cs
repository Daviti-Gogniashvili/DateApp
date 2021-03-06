using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extentions
{
    public static class ApplicationServicesExtention
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration) {
            services.AddScoped<ITokenService, TokenService>();

            services.AddDbContext<DataContext>(options => {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddControllersWithViews()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.WriteIndented = true;
            });

            return services;
        }
    }
}
