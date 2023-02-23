using CollectionsAndLinq.Common.Models;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.BL.Interfaces;

public interface IDataProcessingService
{
    Task<Dictionary<string, int>> GetTasksCountInProjectsByUserId(int userId);
    ICollection<TaskDto> GetCapitalTasksByUserId(int userId);
    Task<Dictionary<int, string>> GetProjectsByTeamSize(int teamSize);
    Task<ICollection<TeamWithMembersDto>> GetSortedTeamByMembersWithYear(int year);
    Task<ICollection<UserWithTasksDto>> GetSortedUsersWithSortedTasks();
    Task<UserInfoDto> GetUserInfo(int userId);
    Task<List<ProjectInfoDto>> GetProjectsInfo();
    Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjects(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel);
}

