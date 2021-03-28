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
    public class AssetDimensionController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetData> CreateAssetDimension([FromBody] AssetData data)
        {
            return Ok(QueryRunner.CreateAssetDimension(data, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetDataConfig> GetAssetDimensions(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssetDimensions(skip, limit));
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateAssetDimension(string id, [FromBody] AssetData data)
        {
            data.Id = id;
            return Ok(QueryRunner.UpdateAssetDimension(data));
        }

        [HttpPost("delete")]
        public ActionResult DeleteAssetDimensions([FromBody] string[] ids)
        {
            QueryRunner.DeleteAssetDimensions(ids, GetUserId());
            return Ok();
        }
    }
}
