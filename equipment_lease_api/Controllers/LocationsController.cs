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
    public class LocationsController : BaseController
    {
        [HttpPost]
        public ActionResult<LocationDTO> CreateLocation([FromBody] LocationDTO location, string subsidiaryId)
        {
            return Ok(QueryRunner.CreateLocation(location, GetUserId()));
        }

        [HttpGet]
        public ActionResult<LocationResult> GetLocations( int skip, int limit)
        {
            return Ok(QueryRunner.GetLocations( skip, limit));
        }

        [HttpPost("{locationId}/update")]
        public ActionResult UpdateLocation(string locationId, [FromBody] LocationDTO location)
        {
            location.Id = locationId;
            return Ok(QueryRunner.UpdateLocation(location));
        }

        [HttpPost("delete")]
        public ActionResult DeleteLocations([FromBody] string[] locationIds)
        {
            QueryRunner.DeleteLocations(locationIds, GetUserId());
            return Ok();
        }
    }
}
