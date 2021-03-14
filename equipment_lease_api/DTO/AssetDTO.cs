using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class AssetDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }

    public class AssetResult
    {
        public List<AssetDTO> Assets { get; set; }
        public int TotalCount { get; set; }
    }
}
