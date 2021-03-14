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
    public class AssetCapacityController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetData> CreateAssetCapacity([FromBody] AssetData data)
        {
            return Ok(QueryRunner.CreateAssetCapacity(data, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetDataConfig> GetAssetCapacities(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssetCapacities(skip, limit));
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateAssetCapacity(string id, [FromBody] AssetData data)
        {
            data.Id = id;
            return Ok(QueryRunner.UpdateAssetCapacity(data));
        }

        [HttpPost("delete")]
        public ActionResult DeleteAssetCapacities([FromBody] string[] ids)
        {
            QueryRunner.DeleteAssetCapacities(ids, GetUserId());
            return Ok();
        }
    }
}
