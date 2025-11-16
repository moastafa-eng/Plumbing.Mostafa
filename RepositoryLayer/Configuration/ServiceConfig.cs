using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RepositoryLayer.Configuration
{
    public class ServiceConfig : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.Property(x => x.CreatedDate).IsRequired().HasMaxLength(10);
            builder.Property(x => x.UpdatedDate).HasMaxLength(10);
            builder.Property(x => x.RowVersion).IsRowVersion(); // Auto updated in DB

            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(2000);
            builder.Property(x => x.Icon).IsRequired().HasMaxLength(100);

            // Data Seed
            builder.HasData(new Service
            {
                Id = 1,
                Name = "Plumbing Service 1",
                Description = "We are a trusted plumbing company offering reliable installation," +
                " maintenance, and repair services." +
                " Our experienced team is dedicated to delivering quality workmanship, timely service," +
                " and complete customer satisfaction for every project we handle.",
                Icon = "bi bi-Service1",
                
            }, new Service
            {
                Id = 2,
                Name = "Plumbing Service 2",
                Description = "We are a trusted plumbing company offering reliable installation," +
                " maintenance, and repair services." +
                " Our experienced team is dedicated to delivering quality workmanship, timely service," +
                " and complete customer satisfaction for every project we handle.",
                Icon = "bi bi-Service2",

            }, new Service
            {
                Id = 3,
                Name = "Plumbing Service 3",
                Description = "We are a trusted plumbing company offering reliable installation," +
                " maintenance, and repair services." +
                " Our experienced team is dedicated to delivering quality workmanship, timely service," +
                " and complete customer satisfaction for every project we handle.",
                Icon = "bi bi-Service3",

            });
        }
    }
}
