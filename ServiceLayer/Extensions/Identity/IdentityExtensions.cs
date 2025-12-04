using EntityLayer.Identity.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;

namespace ServiceLayer.Extensions.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection LoadIdentityExtensions(this IServiceCollection services)
        {
            // Add Identity with configurations
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredUniqueChars = 2;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            })
            // Adds RoleManager<AppRole> to the DI container (optional because AddIdentity already adds it)
            .AddRoleManager<RoleManager<AppRole>>()

            // Tells Identity to use AppDbContext for storing users, roles, and identity tables
            .AddEntityFrameworkStores<AppDbContext>()

            // Adds default token providers (for password reset, email confirmation, 2FA, etc.)
            .AddDefaultTokenProviders();

            // Configure the authentication cookie used by ASP.NET Identity
            services.ConfigureApplicationCookie(opt =>
            {
                // Create a new cookie object
                var newCookie = new CookieBuilder();

                // Set the cookie name (this will appear in the browser)
                newCookie.Name = "PlumbingCompany";

                // Path to redirect the user when trying to access something that requires login
                opt.LoginPath = new PathString("/Authentication/LogIn");

                // Path to redirect the user when they log out
                opt.LogoutPath = new PathString("/Authentication/LogOut");

                // Path to redirect the user if they try to access something without permission
                opt.AccessDeniedPath = new PathString ("/Authentication/AccessDenied");

                // Assign the custom cookie configuration
                opt.Cookie = newCookie;

                // How long the cookie (login session) should remain valid
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(60);
            });

            return services;
        }
    }
}
