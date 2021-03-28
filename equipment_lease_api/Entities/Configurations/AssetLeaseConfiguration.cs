using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class AssetLeaseConfiguration : IEntityTypeConfiguration<AssetLease>
    {
        public void Configure(EntityTypeBuilder<AssetLease> builder)
        {
            builder.HasKey(e => e.NativeId);
            builder.HasAlternateKey(e => e.Id);
            builder.Property(e => e.NativeId).ValueGeneratedOnAdd();
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.ProjectId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.LeaseNumber).HasMaxLength(50).IsUnicode(false);

            builder.HasMany(e => e.AssetLeaseEntries)
                .WithOne(e => e.AssetLease)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.AssetLeaseId);

            builder.HasMany(e => e.AssetLeaseUpdates)
                .WithOne(e => e.AssetLease)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.AssetLeaseId);

            builder
                .HasOne(b => b.Project)
                .WithMany(a => a.AssetLeases)
                .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasOne(b => b.CreatedBy)
            //    .WithMany(a => a.AssetLeases)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
