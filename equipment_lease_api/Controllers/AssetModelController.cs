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
    public class AssetModelController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetData> CreateAssetModel([FromBody] AssetData data)
        {
            return Ok(QueryRunner.CreateAssetModel(data, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetDataConfig> GetAssetModels(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssetModels(skip, limit));
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateAssetModel(string id, [FromBody] AssetData data)
        {
            data.Id = id;
            return Ok(QueryRunner.UpdateAssetModel(data));
        }

        [HttpPost("delete")]
        public ActionResult DeleteAssetModels([FromBody] string[] ids)
        {
            QueryRunner.DeleteAssetModels(ids, GetUserId());
            return Ok();
        }
    }
}
