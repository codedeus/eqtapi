using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class AssetGroupResult
    {
        public List<AssetGroupDTO> AssetGroups { get; set; }
        public int TotalCount { get; set; }
    }

    public class AssetGroupDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string ParentId { get; set; }
        public string ParentGroup { get; set; }
        public string ParentGroupCode { get; set; }
        public bool IsSubGroup { get; set; }
    }
}
