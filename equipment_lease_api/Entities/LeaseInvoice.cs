using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class LeaseInvoice
    {
        public string Id { get; set; }
        public string CreatedById { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedById { get; set; }
        public DateTime LeaseStartDate { get; set; }
        public DateTime LeaseEndDate { get; set; }
        public string LeaseId { get; set; }
        public DateTime CreationDate { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public AppUser CreatedBy { get; set; }
        public ICollection<AssetLeaseEntryUpdate> AssetLeaseEntryUpdates { get; set; }
    }
}
