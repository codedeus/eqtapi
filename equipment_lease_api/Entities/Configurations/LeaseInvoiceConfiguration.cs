using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities.Configurations
{
    public class LeaseInvoiceConfiguration : IEntityTypeConfiguration<LeaseInvoice>
    {
        public void Configure(EntityTypeBuilder<LeaseInvoice> builder)
        {
            builder.HasKey(e => e.NativeId);
            builder.HasAlternateKey(e => e.Id);
            builder.Property(e => e.NativeId).ValueGeneratedOnAdd();
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.AssetLeaseId).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.InvoiceNumber).IsRequired(true).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.InvoicePeriod).IsRequired(true).HasMaxLength(50).IsUnicode(false);
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);

            builder.HasMany(e => e.AssetLeaseUpdates)
                .WithOne(e => e.LeaseInvoice)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction)
                .HasForeignKey(e => e.LeaseInvoiceId);

            builder
                .HasOne(b => b.AssetLease)
                .WithMany(a => a.LeaseInvoices)
                .HasPrincipalKey(d=>d.Id)
                .HasForeignKey(d=>d.AssetLeaseId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
