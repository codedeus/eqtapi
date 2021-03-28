using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class ProjectSiteDTO
    {
        public string Id { get; set; }
        public string SiteName { get; set; }
        public string SiteCode { get; set; }
        public string Project { get; set; }
        public string ProjectId { get; set; }
    }

    public class ProjectSiteResult
    {
        public List<ProjectSiteDTO> ProjectSites { get; set; }
        public int TotalCount { get; set; }
    }
}
