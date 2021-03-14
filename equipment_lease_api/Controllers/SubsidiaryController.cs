using equipment_lease_api.DTO;
using equipment_lease_api.Entities;
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
    [Authorize]
    [ApiController]
    public class SubsidiaryController : BaseController
    {
        [HttpPost]
        public ActionResult<SubsidiaryDTO> CreateSubsidiary([FromBody] SubsidiaryDTO subsidiary)
        {
            return Ok(QueryRunner.AddSubsidiary(subsidiary, GetUserId()));
        }

        [HttpGet]
        public ActionResult<List<SubsidiaryDTO>> GetSubsidiaries()
        {
            return Ok(QueryRunner.GetSubsidiaries());
        }

        [HttpPost("delete")]
        public ActionResult DeleteSubsidiary([FromBody] string[] itemIds)
        {
            QueryRunner.DeleteSubsidiaries(itemIds, GetUserId());
            return Ok();
        }

        [HttpPost("{id}/update")]
        public ActionResult UpdateSubsidiary([FromBody] SubsidiaryDTO subsidiaryDTO, string id)
        {
            return Ok(QueryRunner.UpdateSubsidiary(subsidiaryDTO, id));
        }
    }
}
