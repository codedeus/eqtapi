using equipment_lease_api.DTO;
using equipment_lease_api.Services;
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
    public class AssetTypeController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetTypeDTO> CreateAssetGroup([FromBody] AssetTypeDTO assetType)
        {
            return Ok(QueryRunner.CreateAssetType(assetType, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetTypeResult> GetAssetGroups(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssetTypes(skip, limit));
        }

        [HttpPost("{assetTypeId}/update")]
        public ActionResult UpdateLocation(string assetTypeId, [FromBody] AssetTypeDTO assetType)
        {
            assetType.Id = assetTypeId;
            return Ok(QueryRunner.UpdateAssetType(assetType));
        }

        [HttpPost("delete")]
        public ActionResult DeleteAssetTypes([FromBody] string[] groupIds)
        {
            QueryRunner.DeleteAssetTypes(groupIds, GetUserId());
            return Ok();
        }
    }
}