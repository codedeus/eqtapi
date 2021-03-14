using equipment_lease_api.DTO;
using equipment_lease_api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace equipment_lease_api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : BaseController
    {

        [HttpPost]
        public ActionResult<ProjectDTO> CreateProject([FromBody] ProjectDTO project)
        {
            return Ok(QueryRunner.CreateNewProject(project, GetUserId()));
        }

        [HttpGet]
        public ActionResult GetProjects(string subsidiaryId, string locationId, int skip, int limit)
        {
            return Ok(QueryRunner.GetProjects(locationId, subsidiaryId, skip, limit));
        }

        [HttpPost("delete")]
        public ActionResult DeleteProjects([FromBody] string[] projectIds)
        {
            QueryRunner.DeleteProjects(projectIds, GetUserId());
            return Ok();
        }

        [HttpPost("{projectId}/update")]
        public ActionResult UpdateLocation(string projectId, [FromBody] ProjectDTO project)
        {
            return Ok(QueryRunner.UpdateProject(project, projectId));
        }

        [HttpPost("{projectId}/projectsites")]
        public ActionResult CreateProjectSite(string projectId, [FromBody] ProjectSiteDTO projectsite)
        {
            projectsite.ProjectId = projectId;
            return Ok(QueryRunner.CreateProjectSite(projectsite, GetUserId()));
        }

        [HttpGet("{projectId}/projectsites")]
        public ActionResult GetProjectSites(string projectId)
        {
            return Ok(QueryRunner.GetProjectSites(projectId));
        }

        [HttpPost("projectsites/{siteId}/update")]
        public ActionResult UpdateProjectSites(string siteId,[FromBody]ProjectSiteDTO siteDTO)
        {
            siteDTO.Id = siteId;
            return Ok(QueryRunner.UpdateProjectSite(siteDTO));
        }

        [HttpPost("projectsites/delete")]
        public ActionResult DeleteProjectSites([FromBody] string[] siteIds)
        {
            QueryRunner.DeleteProjectSites(siteIds, GetUserId());
            return Ok();
        }
    }
}
