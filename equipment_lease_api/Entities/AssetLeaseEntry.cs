using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AssetLeaseEntry
    {
        public string Id { get; set; }
        public string CreatedById { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedById { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ExpectedLeaseOutDate { get; set; }
        public string AssetItemId { get; set; }
        public decimal LeaseCost { get; set; }
        public string AssetCurrentStatus { get; set; }
        public DateTime? ActualReturnDate { get; set; }
        public string AssetLeaseId { get; set; }
        public string ProjectSiteId { get; set; }
        public ProjectSite ProjectSite { get; set; }

        //public string ProjectId { get; set; }
        //public string LocationId { get; set; }

        //public Project Project { get; set; }
        //public Location Location { get; set; }

        public AssetLease AssetLease { get; set; }
        public AppUser CreatedBy { get; set; }
        public AppUser DeletedBy { get; set; }
        public AssetItem AssetItem { get; set; }

    }
}
