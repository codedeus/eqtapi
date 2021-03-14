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
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).IsUnicode(false).HasMaxLength(256).ValueGeneratedOnAdd();
            builder.Property(e => e.CreatedById).IsRequired(true).HasMaxLength(256).IsUnicode(false);
            builder.Property(e => e.InvoiceNumber).IsRequired(true).HasMaxLength(20).IsUnicode(false);
            builder.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.DeletedById).HasMaxLength(256).IsUnicode(false);
        }
    }
}
