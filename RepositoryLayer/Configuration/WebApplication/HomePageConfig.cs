using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class HomePageConfig : IEntityTypeConfiguration<HomePage>
    {
        public void Configure(EntityTypeBuilder<HomePage> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion(); // Auto updated in DB

            builder.Property(x => x.Header).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.VideoLink).IsRequired(); // => Link

            // Data Seed
            builder.HasData(new HomePage
            {
                Id = 1,
                Header = "Professional Plumbing Services You Can Rely On",
                Description = "We provide high-quality plumbing solutions for homes and businesses." +
                " Our expert team handles everything from leak repairs and drain cleaning to complete system " +
                "installations — always with honesty, speed, and exceptional care.",
                VideoLink = "Test Video Link"
            });
        }
    }
}
