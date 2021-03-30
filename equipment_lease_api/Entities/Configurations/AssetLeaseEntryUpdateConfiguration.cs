using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class AssetLeaseEntryUpdateConfiguration : IEntityTypeConfiguration<AssetLeaseUpdateEntry>
    {
        public void Configure(EntityTypeBuilder<AssetLeaseUpdateEntry> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetStatus).IsRequired(true).HasMaxLength(20).IsUnicode(false);
            builder.Property(e => e.LastModifiedById).IsRequired(false).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.Comment).IsRequired(false).HasMaxLength(1000).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetLeaseEntryId).HasMaxLength(256).IsUnicode(false);

            //builder
            //    .HasOne(b => b.LeaseInvoice)
            //    .WithMany(a => a.AssetLeaseUpdateEntries)
            //    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
