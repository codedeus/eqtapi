using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class AssetLeaseEntryConfiguration : IEntityTypeConfiguration<AssetLeaseEntry>
    {
        public void Configure(EntityTypeBuilder<AssetLeaseEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetItemId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.LeaseCost).HasColumnType("decimal(18,2)");
            builder.Property(e => e.AssetLeaseId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetCurrentStatus).HasMaxLength(20).IsUnicode(false);
            builder.Property(e => e.ProjectSiteId).IsRequired(true).HasMaxLength(256).IsUnicode(false);


            //builder.Property(e => e.LocationId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            //builder.Property(e => e.ProjectId).IsRequired(true).HasMaxLength(256).IsUnicode(false);

            builder
                .HasOne(b => b.CreatedBy)
                .WithMany(a => a.AssetLeaseEntries)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(b => b.ProjectSite)
                .WithMany(a => a.AssetLeaseEntries)
                .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasOne(b => b.Location)
            //    .WithMany(a => a.AssetLeaseEntries)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasOne(b => b.Project)
            //    .WithMany(a => a.AssetLeaseEntries)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(e => e.AssetItem)
            //    .WithMany(e => e.AssetLeaseEntries)
            //    .HasForeignKey(e => e.AssetItemId)
            //    .HasPrincipalKey(d => d.Id)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
