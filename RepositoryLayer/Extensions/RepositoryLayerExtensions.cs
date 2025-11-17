using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.Repositories.Concrete;
using RepositoryLayer.UnitOfWorks.Abstract;
using RepositoryLayer.UnitOfWorks.Concrete;

namespace RepositoryLayer.Extensions
{
    public static class RepositoryLayerExtensions
    {
        /// <summary>
        /// Extension method for configuring the Repository Layer dependencies.
        /// This method is called once from Program.cs to keep the Startup/Program file clean.
        /// 
        /// Explanation:
        /// - 'this IServiceCollection services' makes this an extension method, 
        ///   so it can be called as 'builder.Services.LoadRepositoryLayerExtensions(...)'.
        /// - 'IConfiguration config' allows reading configuration values (e.g., connection strings).
        /// - Returns IServiceCollection to enable method chaining (e.g., adding more services after this call).
        /// 
        /// Purpose:
        /// Registers the AppDbContext and any repository-related services into the DI container.
        /// </summary>
        public static IServiceCollection LoadRepositoryLayerExtensions(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(config.GetConnectionString("SqlConnection")));
            services.AddScoped(typeof(IGenericRepositories<>), typeof(GenericRepositories<>)); // Open generic type

            // we don't use type of here because IUnitOfWork is a static interface and class UnitOfWork is static
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;


        }
    }
}
