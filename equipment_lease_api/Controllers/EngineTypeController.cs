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
    public class EngineTypeController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetData> CreateEngineType([FromBody] AssetData data)
        {
            return Ok(QueryRunner.CreateEngineType(data, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetDataConfig> GetEngineTypes(int skip, int limit)
        {
            return Ok(QueryRunner.GetEngineTypes(skip, limit));
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateEngineType(string id, [FromBody] AssetData data)
        {
            data.Id = id;
            return Ok(QueryRunner.UpdateEngineType(data));
        }

        [HttpPost("delete")]
        public ActionResult DeleteEngineTypes([FromBody] string[] ids)
        {
            QueryRunner.DeleteEngineTypes(ids, GetUserId());
            return Ok();
        }
    }
}
