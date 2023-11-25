using EGIDTask.Contract.Repository.RepositoryManager;
using EGIDTask.Helpers.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EGIDTask.Repository
{
    public static class StartupSetup
    {

        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager.RepositoryManager>();

            var types = Assembly.GetExecutingAssembly().GetTypes()
                        .Where(p => p.Name.EndsWith("Repository") && !p.IsInterface);

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces(false).FirstOrDefault(i => i.Name.EndsWith("Repository"));
                if (interfaceType != null)
                {
                    services.AddScoped(interfaceType, type);
                }
                else
                {
                    services.AddScoped(type);
                }
            }

        }

    }
}
