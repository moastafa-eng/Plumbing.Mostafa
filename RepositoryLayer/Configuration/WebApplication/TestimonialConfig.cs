using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration.WebApplication
{
    public class TestimonialConfig : IEntityTypeConfiguration<Testimonial>
    {
        public void Configure(EntityTypeBuilder<Testimonial> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion(); // Auto updated in DB

            builder.Property(x => x.FullName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileType).IsRequired();
            builder.Property(x => x.Comment).IsRequired().HasMaxLength(2000);

            // Data Seed
            builder.HasData(new Testimonial 
            {
                Id = 1,
                FullName = "Testimonial Name1",
                Title = "Trusted Testimonial1",
                FileName = "Test",
                FileType = "Test",
                Comment = "the best testimonial at all",
                
            }, new Testimonial
            {
                Id = 2,
                FullName = "Testimonial Name2",
                Title = "Trusted Testimonial2",
                FileName = "Test",
                FileType = "Test",
                Comment = "the best testimonial at all",

            }, new Testimonial
            {
                Id = 3,
                FullName = "Testimonial Name3",
                Title = "Trusted Testimonial3",
                FileName = "Test",
                FileType = "Test",
                Comment = "the best testimonial at all",

            });
        }
    }
}
