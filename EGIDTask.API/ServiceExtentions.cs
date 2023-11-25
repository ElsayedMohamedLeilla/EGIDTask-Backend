using EGIDTask.BackgroundJobs;
using EGIDTask.Data;
using Microsoft.EntityFrameworkCore;

namespace EGIDTask.API
{
    public static class ServiceExtentions
    {
        public static void ConfigureSQLContext(this IServiceCollection services, IConfiguration config)
        {
            _ = services.AddDbContext<ApplicationDBContext>(opts =>
            {
                _ = opts.UseSqlServer(config["ConnectionStrings:EGIDTaskConnectionString"],
                opts => opts.CommandTimeout(60));
                _ = opts.EnableSensitiveDataLogging(true);


            });
            _ = services.AddDbContext<ApplicationDBContext>(options =>
            {
                _ = options.UseSqlServer(
                config.GetConnectionString("EGIDTaskConnectionString"));
                _ = options.EnableSensitiveDataLogging(true);

            }
           );
        }
        public static void ConfigureBackGroundService(this IServiceCollection services)
        {
            services.AddHostedService<UpdateStockPricesBackgroundJob>();
        }

    }
}
