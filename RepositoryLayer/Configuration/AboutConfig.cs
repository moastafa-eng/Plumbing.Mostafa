using EntityLayer.WebApplication.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Configuration
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
        }
    }
}
