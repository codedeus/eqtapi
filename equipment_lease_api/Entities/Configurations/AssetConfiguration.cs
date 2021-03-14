using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace equipment_lease_api.Entities.Configurations
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.Code).HasMaxLength(20).IsUnicode(false);
            builder.Property(e => e.Name).HasMaxLength(256).IsUnicode(false).IsRequired(true);
        }
    }
}
