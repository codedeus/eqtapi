using equipment_lease_api.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class SubsidiaryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class LocationResult
    {
        public List<LocationDTO> Locations { get; set; }
        public int TotalCount { get; set; }
    }

    public class LocationDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public class ProjectDTO
    {
        public string LocationId { get; set; }
        public string Location { get; set; }
        public string Subsidiary { get; set; }
        public string SubsidiaryId { get; set; }
        public DateTime? StartDate { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Id { get; set; }
        public ICollection<ProjectSiteDTO> ProjectSites { get; set; }
    }

    public class ProjectResult
    {
        public List<ProjectDTO> Projects { get; set; }
        public int TotalCount { get; set; }
    }
}
