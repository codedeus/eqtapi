using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.DTO
{
    public class AssetItemDTO
    {
        public string Id { get; set; }
        public string AssetTypeId { get; set; }
        public string SerialNumber { get; set; }
        public string Code { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal? LeaseCost { get; set; }
        public string AssetGroupId { get; set; }
        public string AssetSubGroupId { get; set; }
        public string CurrentStatus { get; set; }
        public string AssetName { get; set; }
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
    }

    public class AssetListResult
    {
        public List<AssetItemView> Items { get; set; }
        public int TotalCount { get; set; }
    }

    public class AssetItemView
    {
        public string Id { get; set; }
        public string AssetTypeId { get; set; }
        public string AssetType { get; set; }
        public string SerialNumber { get; set; }
        public string Code { get; set; }
        public string RegistrationNumber { get; set; }
        public decimal? LeaseCost { get; set; }
        public string AssetGroupId { get; set; }
        public string AssetGroup { get; set; }
        public string AssetSubGroupId { get; set; }
        public string AssetSubGroup { get; set; }
        public string CurrentStatus { get; set; }
        public string AssetId { get; set; }
        public string Asset { get; set; }
        public string AssetBrandId { get; set; }
        public string AssetBrand { get; set; }
        public string AssetModelId { get; set; }
        public string AssetModel { get; set; }
        public string CapacityId { get; set; }
        public string Capacity { get; set; }
        public string DimensionId { get; set; }
        public string Dimension { get; set; }
        public string EngineTypeId { get; set; }
        public string EngineType { get; set; }
        public string EngineModelId { get; set; }
        public string EngineModel { get; set; }
        public string EngineNumber { get; set; }
        public decimal? AssetValue { get; set; }
        public int? ManufactureYear { get; set; }
        public int? AcquisitionYear { get; set; }
        public string CurrentLocationId { get; set; }
        public string CurrentLocation { get; set; }
        public string Remark { get; set; }
    }

    public class AssetBatchUploadDTO
    {
        public string Group { get; set; }
        public string SubGroup { get; set; }
        public string Description { get; set; }
        public string AssetCode { get; set; }
        public string RegistrationNumber { get; set; }
        public string AssetType { get; set; }
        public string AssetBrand { get; set; }
        public string AssetCapacity { get; set; }
        public string AssetModel { get; set; }
        public string AssetDimension { get; set; }
        public string SerialNumber { get; set; }
        public string EngineType { get; set; }
        public string EngineModel { get; set; }
        public string EngineNumber { get; set; }
        public string ManufactorYear { get; set; }
        public string AcquisitionYear { get; set; }
        public decimal? AssetValue { get; set; }
        public string ErrorComment { get; set; }
        public decimal? LeaseCost { get; set; }
    }

    public class BatchUploadResponse
    {
        public bool IsSuccessful { get; set; }
        public object ErrorList { get; set; }
    }
}
