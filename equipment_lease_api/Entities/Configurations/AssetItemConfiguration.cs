using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class AssetItemConfiguration : IEntityTypeConfiguration<AssetItem>
    {
        public void Configure(EntityTypeBuilder<AssetItem> builder)
        {
            //builder.HasKey(e => e.NativeId);
            //builder.HasAlternateKey(e => e.Id);
            //builder.Property(e => e.NativeId).ValueGeneratedOnAdd();
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.LeaseCost).HasColumnType("decimal(18,2)");
            builder.Property(e => e.AssetValue).HasColumnType("decimal(18,2)");
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetGroupId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetSubGroupId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.CurrentStatus).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.RegistrationNumber).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.AssetTypeId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.Code).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.SerialNumber).HasMaxLength(100).IsUnicode(false);
            builder.Property(e => e.AssetId).HasMaxLength(256).IsUnicode(false);

            builder.Property(e => e.AssetBrandId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetModelId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.CapacityId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DimensionId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.EngineTypeId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.EngineModelId).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.EngineNumber).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.CurrentLocationId).HasMaxLength(256).IsUnicode(false);

            builder
                .HasOne(b => b.CreatedBy)
                .WithMany(a => a.AssetItems)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
