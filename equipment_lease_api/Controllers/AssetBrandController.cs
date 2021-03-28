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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AssetBrandController : BaseController
    {
        [HttpPost]
        public ActionResult<AssetBrandDTO> CreateAssetGroup([FromBody] AssetBrandDTO assetBrandDTO)
        {
            return Ok(QueryRunner.CreateAssetBrand(assetBrandDTO, GetUserId()));
        }

        [HttpGet]
        public ActionResult<AssetBrandResult> GetAssetGroups(int skip, int limit)
        {
            return Ok(QueryRunner.GetAssetBrands(skip, limit));
        }

        [HttpPost("{brandId}/update")]
        public ActionResult UpdateLocation(string brandId, [FromBody] AssetBrandDTO assetBrand)
        {
            assetBrand.Id = brandId;
            return Ok(QueryRunner.UpdateAssetBrand(assetBrand));
        }

        [HttpPost("delete")]
        public ActionResult DeleteAssetBrands([FromBody] string[] groupIds)
        {
            QueryRunner.DeleteAssetBrands(groupIds, GetUserId());
            return Ok();
        }
    }
}