using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.Common.Models;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataProcessingController : ControllerBase
    {
        private readonly IDataProcessingService _dataProcessingService;

        public DataProcessingController(IDataProcessingService dataProcessingService)
        {
            _dataProcessingService = dataProcessingService;
        }

        [HttpGet("GetCapitalTasksByUserId/{userId}")]
        public ActionResult<ICollection<TaskDto>> GetCapitalTasksByUserId(int userId)
        {
            return Ok(_dataProcessingService.GetCapitalTasksByUserId(userId));
        }

        [HttpGet("GetProjectsByTeamSize/{teamSize}")]
        public async Task<ActionResult<Dictionary<int, string>>> GetProjectsByTeamSize(int teamSize)
        {
            return Ok(await _dataProcessingService.GetProjectsByTeamSize(teamSize));
        }

        [HttpGet("GetProjectsInfo")]
        public async Task<ActionResult<List<ProjectInfoDto>>> GetProjectsInfo()
        {
            return Ok(await _dataProcessingService.GetProjectsInfo());
        }

        [HttpGet("GetSortedTeamByMembersWithYear/{year}")]
        public async Task<ActionResult<ICollection<TeamWithMembersDto>>> GetSortedTeamByMembersWithYear(int year)
        {
            return Ok(await _dataProcessingService.GetSortedTeamByMembersWithYear(year));
        }

        [HttpGet("GetSortedUsersWithSortedTasks")]
        public async Task<ActionResult<ICollection<UserWithTasksDto>>> GetSortedUsersWithSortedTasks()
        {
            return Ok(await _dataProcessingService.GetSortedUsersWithSortedTasks());
        }

        [HttpGet("GetTasksCountInProjectsByUserId/{userId}")]
        public async Task<ActionResult<Dictionary<string, int>>> GetTasksCountInProjectsByUserId(int userId)
        {
            return Ok(await _dataProcessingService.GetTasksCountInProjectsByUserId(userId));
        }

        [HttpGet("GetUserInfo/{userId}")]
        public async Task<ActionResult<UserInfoDto>> GetUserInfo(int userId)
        {
            return Ok(await _dataProcessingService.GetUserInfo(userId));
        }

        [HttpGet("GetSortedFilteredPageOfProjects")]
        public async Task<ActionResult<PagedList<FullProjectDto>>> GetSortedFilteredPageOfProjects([FromQuery] GetRequest request)
        {
            return Ok(await _dataProcessingService.GetSortedFilteredPageOfProjects(request.PageModel, request.FilterModel, request.SortingModel));
        }
    }
}
