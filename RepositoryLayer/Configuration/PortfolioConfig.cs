using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class PortfolioConfig : IEntityTypeConfiguration<Portfolio>
    {
        public void Configure(EntityTypeBuilder<Portfolio> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion(); // Auto updated in DB

            builder.Property(x => x.Title).IsRequired().HasMaxLength(200);
            builder.Property(x => x.FileName).IsRequired();
            builder.Property(x => x.FileType).IsRequired();

            // Data Seed
            builder.HasData(new Portfolio
            {
                Id = 1,
                Title = "Test Picture",
                FileName = "Test",
                FileType = "Test",
                CategoryId = 1,

            }, new Portfolio
            {
                Id = 2,
                Title = "Test Picture2",
                FileName = "Test2",
                FileType = "Test2",
                CategoryId = 1,

            }, new Portfolio
            {
                Id = 3,
                Title = "Test Picture3",
                FileName = "Test3",
                FileType = "Test3",
                CategoryId = 2,

            }, new Portfolio
            {
                Id = 4,
                Title = "Test Picture4",
                FileName = "Test4",
                FileType = "Test4",
                CategoryId = 2,

            });
        }
    }
}
