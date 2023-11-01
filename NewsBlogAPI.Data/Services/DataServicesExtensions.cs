using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NewsBlogAPI.Data.Interfaces;
using NewsBlogAPI.Data.Repository;

namespace NewsBlogAPI.Data.Services
{
    public static class DataServicesExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, string dbConnectionString)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(dbConnectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            services.BuildServiceProvider().GetService<DataContext>().Database.Migrate();
            return services;
        }
    }
}
