using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Entities
{
    public class AssetItem
    {
        //public int NativeId { get; set; }
        public string Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedById { get; set; }
        public bool IsDeleted { get; set; }
        public string DeletedById { get; set; }
        public string AssetTypeId { get; set; }
        public string SerialNumber { get; set; }
        public string Code { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal? LeaseCost { get; set; }
        public string AssetGroupId { get; set; }
        public string AssetSubGroupId { get; set; }
        public string CurrentStatus { get; set; }
        public string AssetId { get; set; }
        public string AssetBrandId { get; set; }
        public string AssetModelId { get; set; }
        public string CapacityId { get; set; }
        public string DimensionId { get; set; }
        public string EngineTypeId { get; set; }
        public string EngineModelId { get; set; }
        public string EngineNumber { get; set; }
        public decimal? AssetValue { get; set; }
        public int? ManufactureYear { get; set; }
        public int? AcquisitionYear { get; set; }
        public string CurrentLocationId { get; set; }
        public string Remark { get; set; }
        public Location CurrentLocation { get; set; }
        public EngineModel EngineModel { get; set; }
        public EngineType EngineType { get; set; }
        public AssetDimension Dimension { get; set; }
        public AssetCapacity Capacity { get; set; }
        public AssetModel AssetModel { get; set; }
        public AssetBrand AssetBrand { get; set; }
        public Asset Asset { get; set; }
        public AssetType AssetType { get; set; }
        public AssetGroup AssetGroup { get; set; }
        public AssetGroup AssetSubGroup { get; set; }
        public AppUser CreatedBy { get; set; }
        public AppUser DeletedBy { get; set; }
        public ICollection<AssetLeaseEntry> AssetLeaseEntries { get; set; }
    }
}
