using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class AssetLeaseDTO
    {
        public string Id { get; set; }
        public string SubsidiaryId { get; set; }
        public string ProjectId { get; set; }
        public string LeaseNumber { get; set; }
        public DateTime LeaseDate { get; set; }
        //public string LocationId { get; set; }
        //public string ProjectSiteId { get; set; }
        public List<AssetLeaseItem> AssetList { get; set; }
    }

    public class AssetLeaseItem
    {
        public string Id { get; set; }
        public DateTime ExpectedReturnDate { get; set; }
        public DateTime ExpectedLeaseOutDate { get; set; }
        public decimal LeaseCost { get; set; }
        public string AssetId { get; set; }

        //public string LocationId { get; set; }
        public string ProjectSiteId { get; set; }
        public string AssetName { get; set; }
        public string AssetBrand { get; set; }
        public string AssetCode { get; set; }
        public string SerialNumber { get; set; }
        public string ProjectSite { get; set; }
    }
}
