using RepositoryLayer.Extensions;
using ServiceLayer.Extensions;

namespace Plumbing.Mostafa.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.LoadRepositoryLayerExtensions(builder.Configuration); // Load all Repository Services.
            builder.Services.LoadServiceLayerExtensions(); // Load all Service Services.
                

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Application middle ware's 
            app.UseHttpsRedirection(); // Redirect to HTTPS
            app.UseRouting(); // Prepare Routing

            app.UseAuthorization(); // Check Authorization 

            app.MapStaticAssets(); // Enable static files

#pragma warning disable ASP0014 // Ignore The Warning
            app.UseEndpoints(endpoint =>
            {
                endpoint.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

                endpoint.MapAreaControllerRoute(
                    name: "User",
                    areaName: "User",
                    pattern: "User/{controller=Dashboard}/{action=Index}/{id?}");

                endpoint.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.Run(); 
            #endregion
        }
    }
}
