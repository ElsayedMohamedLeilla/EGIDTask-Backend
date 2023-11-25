using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EGIDTask.Validation
{
    public static class StartupSetup
    {
        public static void ConfigureBLValidation(this IServiceCollection services)
        {
            var types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(p => p.Name.EndsWith("BLValidation") && !p.IsInterface);

            foreach (var type in types)
            {
                var interfaceType = type.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("BLValidation"));
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
