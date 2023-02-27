using CollectionsAndLinq.Common.Models.Tasks;

namespace CollectionsAndLinq.Common.Models.Projects;

public record ProjectInfoDto(
    ProjectDto Project,
    TaskDto LongestTaskByDescription,
    TaskDto ShortestTaskByName,
    int? TeamMembersCount = null)
{

}
