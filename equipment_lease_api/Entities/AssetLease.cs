using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AssetLease
    {
        public string Id { get; set; }
        public int NativeId { get; set; }
        public string CreatedById { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedById { get; set; }
        public DateTime CreationDate { get; set; }
        public string LeaseNumber { get; set; }

        public string ProjectId { get; set; }
        public Project Project { get; set; }
        public AppUser CreatedBy { get; set; }
        public AppUser DeletedBy { get; set; }
        public ICollection<AssetLeaseEntry> AssetLeaseEntries { get; set; }
    }
}
