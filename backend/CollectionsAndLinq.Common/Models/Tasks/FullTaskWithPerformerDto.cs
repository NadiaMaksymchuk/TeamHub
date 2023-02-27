using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.Common.Models.Tasks
{
    public record FullTaskWithPerformerDto(
    int Id,
    int ProjectId,
    string Name,
    string Description,
    string State,
    DateTime CreatedAt,
    DateTime? FinishedAt,
    UserDto Perfomer)
    {

    }
}
