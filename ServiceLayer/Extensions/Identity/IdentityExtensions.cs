using EntityLayer.Identity.Entities;
using EntityLayer.Identity.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Context;
using ServiceLayer.Helpers.Identity.EmailHelper;

namespace ServiceLayer.Extensions.Identity
{
    public static class IdentityExtensions
    {
        public static IServiceCollection LoadIdentityExtensions(this IServiceCollection services, IConfiguration config)
        {
            // Add Identity with configurations
            services.AddIdentity<AppUser, AppRole>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequiredUniqueChars = 2;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(60);
                opt.Lockout.MaxFailedAccessAttempts = 3;
            }) 
            .AddRoleManager<RoleManager<AppRole>>() // Adds RoleManager<AppRole> to the DI container (optional because AddIdentity already adds it)
            .AddEntityFrameworkStores<AppDbContext>() // Tells Identity to use AppDbContext for storing users, roles, and identity tables    
            .AddDefaultTokenProviders(); // Adds default token providers (for password reset, email confirmation, 2FA, etc.)

            // Configure the authentication cookie used by ASP.NET Identity
            services.ConfigureApplicationCookie(opt =>
            {
                var newCookie = new CookieBuilder(); // Create a new cookie object

                newCookie.Name = "PlumbingCompany"; // Set the cookie name (this will appear in the browser)
                opt.LoginPath = new PathString("/Authentication/SignIn"); // Path to redirect the user when trying to access something that requires login   
                opt.LogoutPath = new PathString("/Authentication/SignOut"); // Path to redirect the user when they log out
                opt.AccessDeniedPath = new PathString ("/Authentication/AccessDenied"); // Path to redirect the user if they try to access something without permission

                // Assign the custom cookie configuration
                opt.Cookie = newCookie;

                
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(60); // How long the cookie (login session) should remain valid
            });

            // Get Gmail Information from AppSettings file and sign this information to GmailInformationVM when we use IOption
            services.Configure<GmailInformationVM>(config.GetSection("EmailSettings")); // GetSection : Get information from AppSetting file [EmailSettings Section]

            // Life time of token
            services.Configure<DataProtectionTokenProviderOptions>(opt =>
            {
                opt.TokenLifespan = TimeSpan.FromMinutes(60); 
            });

            // Add EmailSendMethod to DI container
            services.AddScoped<IEmailSendMethod, EmailSendMethod>();

            return services;
        }
    }
}
