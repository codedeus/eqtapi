using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AssetLeaseEntryUpdate
    {
        public string Id { get; set; }
        public string CreatedById { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedById { get; set; }
        public DateTime CreationDate { get; set; }
        public string AssetStatus { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool InvoiceRaised { get; set; }
        public string LeaseInvoiceId { get; set; }
        public string Comment { get; set; }
        public string AssetLeaseEntryId { get; set; }
        public AssetLeaseEntry AssetLeaseEntry { get; set; }
        public AppUser CreatedBy { get; set; }
        public AppUser DeletedBy { get; set; }
        public LeaseInvoice LeaseInvoice { get; set; }
    }
}
