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

    public class LeaseEntryUpdateRequest
    {
        public string Id { get; set; }
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
    }

    public class LeaseUpdateRequest
    {
        public List<LeaseEntryUpdateRequest> Entries { get; set; }
        public string Id { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
