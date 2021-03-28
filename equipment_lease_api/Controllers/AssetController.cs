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
    public class AssetController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetDTO> CreateAssetGroup([FromBody] AssetDTO asset)
        {
            return Ok(QueryRunner.CreateAsset(asset, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetTypeResult> GetAssetGroups(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssets(skip, limit));
        }

        [HttpPost("{assetTypeId}/update")]
        public ActionResult UpdateLocation(string assetTypeId, [FromBody] AssetDTO asset)
        {
            asset.Id = assetTypeId;
            return Ok(QueryRunner.UpdateAsset(asset));
        }

        [HttpPost("delete")]
        public ActionResult DeleteAssetTypes([FromBody] string[] assetIds)
        {
            QueryRunner.DeleteAssets(assetIds, GetUserId());
            return Ok();
        }

        [HttpGet("search")]
        public ActionResult SearchForAssets(string searchText)
        {
            return Ok(QueryRunner.SearchForAssets(searchText));
        }
    }
}
