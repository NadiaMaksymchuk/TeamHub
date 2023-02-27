using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<ProjectDto>>> GetAll()
        {
            return Ok(await _projectService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> Add(NewProjectDto projectDto)
        {
            return Ok(await _projectService.AddProject(projectDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(UpdateProjectDto projectDto)
        {
            await _projectService.UpdateProject(projectDto);

            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetById(int id)
        {
            return Ok(await _projectService.GetById(id));
        }
    }
}
