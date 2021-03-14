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
    public class AssetGroupController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetGroupDTO> CreateAssetGroup([FromBody] AssetGroupDTO location)
        {
            return Ok(QueryRunner.CreateAssetGroup(location, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetGroupResult> GetAssetGroups(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssetGroups(skip, limit));
        }

        [HttpPost("{groupId}/update")]
        public ActionResult UpdateLocation(string groupId, [FromBody] AssetGroupDTO assetGroup)
        {
            assetGroup.Id = groupId;
            return Ok(QueryRunner.UpdateAssetGroup(assetGroup));
        }

        [HttpPost("delete")]
        public ActionResult DeleteLocations([FromBody] string[] groupIds)
        {
            QueryRunner.DeleteAssetGroups(groupIds, GetUserId());
            return Ok();
        }

        [HttpGet("{groupId}/subgroups")]
        public ActionResult GetAssetSubGroups(int skip, int limit, string groupId)
        {
            return Ok(QueryRunner.GetAssetSubGroups(skip, limit, groupId));
        }
    }
}
