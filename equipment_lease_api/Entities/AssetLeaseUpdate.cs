using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AssetLeaseUpdate
    {
        public string Id { get; set; }
        public string CreatedById { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedById { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DateDeleted { get; set; }
        public bool InvoiceRaised { get; set; }
        public string AssetLeaseId { get; set; }
        public string LeaseInvoiceId { get; set; }
        public AssetLease AssetLease { get; set; }
        public LeaseInvoice LeaseInvoice { get; set; }
        public AppUser CreatedBy { get; set; }
        public ICollection<AssetLeaseUpdateEntry> AssetLeaseUpdateEntries { get; set; }
    }
}
