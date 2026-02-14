using EntityLayer.Identity.Entities;
using EntityLayer.WebApplication.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace RepositoryLayer.Context
{
    // IdentityDbContext : it contains all class for AppUser & AppRole and it takes 
    // type of primary key to Register them in data base.
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        #region Constructors
		
        public AppDbContext(DbContextOptions options) : base(options) { }
        public AppDbContext() { }
        #endregion

        #region DbSets

        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<HomePage> HomePages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<SocialMedia> SocialMedias { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Testimonial> Testimonial { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Comment About Assembly
            // Applies all configuration classes in the current assembly (DLL) to the model builder.
            // It finds all IEntityTypeConfiguration implementations in the current assembly (DLL) 
            // and applies them to the DbContext.
            // The current assembly is typically the project's compiled DLL (e.g., .exe or .dll file).
            // A DLL (Dynamic Link Library) is a compiled file that contains executable code and resources
            // which can be shared and used by multiple applications. It's used for modular programming
            // to allow multiple programs to share functionality without duplication.
            #endregion
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            #region OnModelCreating
            // Called by EF Core when building the database model.
            // Used to configure entity mappings, relationships, and apply all Fluent API configurations.
            // base.OnModelCreating is required to keep Identity tables and relationships working correctly. 
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
