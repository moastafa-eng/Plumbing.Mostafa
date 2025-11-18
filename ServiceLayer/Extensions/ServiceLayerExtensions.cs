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

            return services;
        }
    }
}
