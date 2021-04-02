using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Models
{
    public class InvoiceDetail
    {
        public string Id { get; internal set; }
        public string InvoiceNumber { get; internal set; }
        public string InvoicePeriod { get; internal set; }
        public string AssetLeaseId { get; internal set; }
        public string Project { get; internal set; }
        public string Location { get; internal set; }
        public DateTime InvoiceDate { get; internal set; }
        public decimal TotalAmount => InvoiceEntries.Sum(s => s.Amount);
        public List<InvoiceEntry> InvoiceEntries { get; set; }
    }

    public class InvoiceEntry
    {
        public string Description { get; internal set; }
        public string AssetCode { get; internal set; }
        public string AssetBrand { get; internal set; }
        public string AssetGroup { get; internal set; }
        public string AssetModel { get; internal set; }
        public string AssetSubGroup { get; internal set; }
        public string AssetType { get; internal set; }
        public string AssetCapacity { get; internal set; }
        public string AssetSerialNumber { get; internal set; }
        public string AssetDimension { get; internal set; }
        public int UpTime { get; internal set; }
        public decimal LeaseCost { get; internal set; }
        public int DownTime { get; internal set; }
        public string AssetItemId { get; internal set; }
        public string AssetLeaseEntryId { get; internal set; }
        public decimal Amount => UpTime * LeaseCost;
    }
}
