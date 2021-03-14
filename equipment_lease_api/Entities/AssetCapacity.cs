using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AssetCapacity
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? DateDeleted { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string CreatedById { get; set; }
        public string DeletedById { get; set; }
        public AppUser DeletedBy { get; set; }
        public AppUser CreatedBy { get; set; }
    }
}
