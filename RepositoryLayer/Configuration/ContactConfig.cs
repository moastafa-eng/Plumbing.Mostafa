using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class ContactConfig : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion(); // Auto updated in DB

            builder.Property(x => x.Location).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Call).IsRequired().HasMaxLength(13); // +201555301076
            builder.Property(x => x.Map).IsRequired(); // Link

            // Data Seed
            builder.HasData(new Contact
            {
                Id = 1,
                Location = "Cairo, NasrCity, AbasStreet, 11OmarLotfy",
                Email = "mostafa.fayez@gmail.com",
                Call = "01555301076",
                Map = "Test Map Link",

            });
        }
    }
}
