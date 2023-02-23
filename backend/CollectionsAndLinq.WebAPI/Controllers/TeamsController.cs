using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TeamDto>>> GetAll()
        {
            return Ok(await _teamService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> AddTeam(NewTeamDto teamDto)
        {
            return Ok(await _teamService.AddTeam(teamDto));
        }

        [HttpPut] 
        public async Task<IActionResult> UpdateTeam(UpdateTeamDto teamDto)
        {
            await _teamService.UpdateTeam(teamDto);

            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeam(id);

            return NoContent();
        }

        [HttpGet("id")]
        public async Task<ActionResult<TeamDto>> GetById(int id)
        {
           return Ok(await _teamService.GetById(id));
        }
    }
}
