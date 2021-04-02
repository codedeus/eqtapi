using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Models
{
    public class LeaseInvoiceRequest
    {
        public DateTime UpdateStartDate { get; set; }
        public DateTime UpdateEndDate { get; set; }
        public string AssetLeaseId { get; set; }
        public string InvoicePeriod { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
