using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.Common.Models.Tasks;

public record TaskDto(
    int Id,
    string Name,
    string Description,
    string State,
    DateTime CreatedAt,
    DateTime? FinishedAt,
    ProjectDto Project,
    UserDto Performer)
{

}
