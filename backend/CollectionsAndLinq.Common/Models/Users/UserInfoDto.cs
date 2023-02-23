using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;

namespace CollectionsAndLinq.Common.Models.Users;

public record UserInfoDto(
    UserDto User,
    ProjectDto LastProject,
    int LastProjectTasksCount,
    int NotFinishedOrCanceledTasksCount,
    TaskDto LongestTask)
{

}
