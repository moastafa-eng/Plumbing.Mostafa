using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ServiceLayer.Extensions
{
    public static class ServiceLayerExtensions
    {
        public static IServiceCollection LoadServiceLayerExtensions(this IServiceCollection services)
        {
            // Find All Configurations in Assembly file in Service layer.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Get all non-abstract classes that end with "Service from Currently Assembly.
            var types = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && !x.IsAbstract &&
            x.Name.EndsWith("Service"));

            // Auto-register each service with its matching interface (I + class name)
            foreach (var serviceType in types)
            {
                // Try to find the interface that matches the pattern: (I + ServiceName)
                var iServiceType = serviceType.GetInterfaces().FirstOrDefault(x => x.Name == $"I{serviceType.Name}");

                // If matching interface exists, register service in DI container
                if (iServiceType is not null)
                {
                    services.AddScoped(iServiceType, serviceType);
                }
            }

            return services;
        }
    }
}
