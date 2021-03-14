using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class AssetDataConfig
    {
        public List<AssetData> Data { get; set; }
        public int TotalCount { get; set; }
    }

    public class AssetData
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
