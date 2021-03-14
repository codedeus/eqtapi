using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.LocationId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.SubsidiaryId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.Name).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.Code).HasMaxLength(256).IsUnicode(false);

            builder
                .HasOne(b => b.Location)
                .WithMany(a => a.Projects)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(b => b.Subsidiary)
                .WithMany(a => a.Projects)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
