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
    public class AssetLeaseController : BaseController
    {
        [HttpPost("new")]
        public ActionResult CreateLeaseOrder([FromBody] AssetLeaseDTO assetLeaseDTO)
        {
            return Ok(LeaseQuery.CreateNewAssetLease(assetLeaseDTO, GetUserId()));
        }

        [HttpGet("{id}/edit")]
        public ActionResult GetLeaseDetails(string id)
        {
            return Ok(LeaseQuery.GetAssetLeaseEntries(id));
        }

        [HttpGet("search")]
        public ActionResult SearchForLeaseNumber(string searchText)
        {
           
            return Ok(LeaseQuery.SearchForLeaseNumbers(searchText));
        }

        [HttpPost("update")]
        public ActionResult UpdateAssetLease([FromBody] AssetLeaseDTO assetLeaseDTO)
        {
            return Ok(LeaseQuery.UpdateAssetLease(assetLeaseDTO, GetUserId()));
        }

    }
}
