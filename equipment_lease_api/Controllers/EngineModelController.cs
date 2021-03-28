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
    public class EngineModelController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetData> CreateEngineModel([FromBody] AssetData data)
        {
            return Ok(QueryRunner.CreateEngineModel(data, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetDataConfig> GetEngineModels(int skip, int limit)
        {
            return Ok(QueryRunner.GetEngineModels(skip, limit));
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateEngineModel(string id, [FromBody] AssetData data)
        {
            data.Id = id;
            return Ok(QueryRunner.UpdateEngineModel(data));
        }

        [HttpPost("delete")]
        public ActionResult DeleteEngineModels([FromBody] string[] ids)
        {
            QueryRunner.DeleteEngineModels(ids, GetUserId());
            return Ok();
        }
    }
}
