using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class AboutConfig : IEntityTypeConfiguration<About>
    {
        public void Configure(EntityTypeBuilder<About> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion(); // Auto updated in DB

            builder.Property(x => x.Header).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(5000);
            builder.Property(x => x.Projects).IsRequired().HasMaxLength(5);
            builder.Property(x => x.HourOfSupport).IsRequired().HasMaxLength(5);
            builder.Property(x => x.HardWorkers).IsRequired().HasMaxLength(5);
            builder.Property(x => x.Clients).IsRequired().HasMaxLength(5);

            // Data Seed
            builder.HasData(new About
            {
                Id = 1,
                Header = "Professional Plumbing Solutions You Can Trust",
                Description = "We are a trusted plumbing company offering reliable installation," +
                " maintenance, and repair services." +
                " Our experienced team is dedicated to delivering quality workmanship, timely service," +
                " and complete customer satisfaction for every project we handle.",
                Clients = 5,
                Projects = 5,
                HourOfSupport = 150,
                HardWorkers = 3,
                FileName = "Test",
                FileType = "Test",
                SocialMediaId = 1,
            });
        }
    }
}
