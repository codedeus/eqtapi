using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class Project
    {
        public string Id { get; set; }
        public string LocationId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime? DateDeleted { get; set; }
        public DateTime? StartDate { get; set; }
        public string CreatedById { get; set; }
        public string DeletedById { get; set; }
        public string SubsidiaryId { get; set; }
        public AppUser DeletedBy { get; set; }
        public AppUser CreatedBy { get; set; }
        public Location Location { get; set; }
        public Subsidiary Subsidiary { get; set; }
        public ICollection<AssetLease> AssetLeases { get; set; }
        public ICollection<ProjectSite> ProjectSites { get; set; }
    }
}
