using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddMyRepositories(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext, AppDbContext>();
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDebtorsRepository, DebtorsRepository>();

            return services;
        }
    }
}
