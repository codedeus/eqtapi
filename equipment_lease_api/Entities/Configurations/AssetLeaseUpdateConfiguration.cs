using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class AssetLeaseUpdateConfiguration : IEntityTypeConfiguration<AssetLeaseUpdate>
    {
        public void Configure(EntityTypeBuilder<AssetLeaseUpdate> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetLeaseId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);

            builder.HasOne(e => e.LeaseInvoice)
                .WithMany(e => e.AssetLeaseUpdates)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(b => b.AssetLease)
                .WithMany(a => a.AssetLeaseUpdates)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            //builder
            //    .HasOne(b => b.CreatedBy)
            //    .WithMany(a => a.AssetLeaseUpdates)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
