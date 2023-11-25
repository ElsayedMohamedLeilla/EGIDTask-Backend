using EGIDTask.Data.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace EGIDTask.Data
{
    public static class StartupSetup
    {
        public static void ConfigureRepositoryContainer(this IServiceCollection services)
        {
            services.AddScoped<ApplicationDBContext>();
            services.AddScoped<IUnitOfWork<ApplicationDBContext>, UnitOfWork<ApplicationDBContext>>();

        }
    }
}
