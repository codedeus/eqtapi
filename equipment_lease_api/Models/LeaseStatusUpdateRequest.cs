using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Models
{
    public class LeaseStatusUpdateRequest
    {
        public string Id { get; set; }
        public string AssetStatus { get; set; }
    }

    public class LeaseUpdateDetails
    {
        public string Project { get; set; }
        public string Location { get; set; }
        public string Subsidiary { get; set; }
        public string LeaseNumber { get; set; }
        public string AssetLeaseId { get; set; }
        public DateTime ExpectedLeaseOutDate => Entries.OrderBy(d => d.ExpectedLeaseOutDate).Select(d => d.ExpectedLeaseOutDate).FirstOrDefault();
        public List<LeaseEntryUpdateRequest> Entries { get; set; }
    }

    public class LeaseEntryUpdateRequest
    {
        public string AssetLeaseEntryId { get; set; }
        public string AssetGroup { get; set; }
        public string AssetSubGroup { get; set; }
        public string Description { get; set; }
        public string AssetCode { get; set; }
        public string AssetType { get; set; }
        public string AssetBrand { get; set; }
        public string AssetModel { get; set; }
        public string AssetCapacity { get; set; }
        public string AssetSerialNumber { get; set; }
        public string EngineModel { get; set; }
        public string EngineSerialNumber { get; set; }
        public string FunctionalStatus { get; set; }
        public string Remark { get; set; }
        internal DateTime ExpectedLeaseOutDate { get; set; }
    }

    public class LeaseUpdateRequest
    {
        public List<LeaseEntryUpdateRequest> Entries { get; set; }
        public string AssetLeaseId { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class LeaseUpdateExcelUpload
    {
        public List<ExcelUpdateEntry> Entries { get; set; }
        public string AssetLeaseId { get; set; }
        public DateTime UpdateDate { get; set; }
    }

    public class ExcelUpdateEntry
    {
        public string AssetCode { get; set; }
        public string FunctionalStatus { get; set; }
        public string Remark { get; set; }
        public string ErrorComment { get; set; }
    }
}
