using equipment_lease_api.DTO;
using equipment_lease_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace equipment_lease_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AssetItemController : BaseController
    {
        [HttpPost]
        public ActionResult CreateNewAsset([FromBody] AssetItemDTO assetItemDTO)
        {
            return Ok(QueryRunner.SaveAssetItem(assetItemDTO, GetUserId()));
        }

        [HttpGet]
        public ActionResult GetAssetItems(int skip, int limit, string assetGroupId = null,
            string assetSubGroupId = null,
            string assetBrandId = null,
            string assetTypeId = null,
            string assetModelId = null,
            string assetCapacityId = null,
            string assetDimensionId = null,
            string engineTypeId = null,
            string engineModelId = null,
            string assetDescriptionId = null)
        {
            return Ok(QueryRunner.GetAssetItems(skip, limit, assetGroupId,
             assetSubGroupId,
             assetBrandId,
             assetTypeId,
             assetModelId,
             assetCapacityId,
             assetDimensionId,
             engineTypeId,
             engineModelId,
             assetDescriptionId));
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateAsset([FromBody]AssetItemDTO assetItemDTO, string id)
        {
            return Ok(QueryRunner.UpdateAssetItem(assetItemDTO, id));
        }

        [HttpPost("delete")]
        public ActionResult DeleteItems([FromBody] string[] itemIds)
        {
            QueryRunner.DeleteAssetItems(itemIds, GetUserId());
            return Ok();
        }

        [HttpGet("search")]
        public ActionResult SearchForItems(string searchText)
        {
            return Ok(QueryRunner.SearchForItems(searchText));
        }

        [HttpPost("batch")]
        public ActionResult BatchUploadAssets([FromBody]List<AssetBatchUploadDTO> batchUploadDTOs)
        {
            return Ok(QueryRunner.BatchUploadAssetItems(batchUploadDTOs, GetUserId()));
        }
    }
}
