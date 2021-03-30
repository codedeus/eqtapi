using equipment_lease_api.Models;
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
    public class LeaseUpdateController : BaseController
    {
        [HttpGet("leases/{leaseId}/updates")]
        public ActionResult GetLeaseUpdates(string leaseId)
        {
            return Ok(LeaseUpdateQuery.GetLeaseUpdates(leaseId));
        }

        [HttpPost("leases/{leaseId}/updates")]
        public ActionResult CreateLeaseUpdate([FromBody] LeaseUpdateRequest leaseUpdate, string leaseId)
        {
            leaseUpdate.AssetLeaseId = leaseId;
            return Ok(LeaseUpdateQuery.CreateLeaseUpdate(leaseUpdate,GetUserId()));
        }

        [HttpPost("leases/{leaseId}/excel-updates")]
        public ActionResult AssetLeaseExcelUpdate([FromBody] LeaseUpdateExcelUpload leaseUpdate, string leaseId)
        {
            leaseUpdate.AssetLeaseId = leaseId;
            return Ok(LeaseUpdateQuery.AssetLeaseExcelUpdate(leaseUpdate, GetUserId()));
        }

        [HttpGet("leases/{leaseId}")]
        public ActionResult GetLeaseEntriesForUpdate(string leaseId)
        {
            return Ok(LeaseUpdateQuery.GetLeaseEntriesForUpdate(leaseId));
        }
        

        [HttpGet("{leaseUpdateId}")]
        public ActionResult GetLeaseUpdateEntries(string leaseUpdateId)
        {
            return Ok(LeaseUpdateQuery.GetLeaseUpdateEntries(leaseUpdateId));
        }

        [HttpPost("{leaseUpdateId}/update")]
        public ActionResult UpdateLeaseStatus(string leaseUpdateId,[FromBody]List<LeaseStatusUpdateRequest> updateRequests)
        {
            return Ok(LeaseUpdateQuery.UpdateLeaseUpdateStatus(updateRequests, GetUserId()));
        }
    }
}
