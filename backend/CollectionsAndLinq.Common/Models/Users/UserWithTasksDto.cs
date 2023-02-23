using CollectionsAndLinq.Common.Models.Tasks;

namespace CollectionsAndLinq.Common.Models.Users;

public record UserWithTasksDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime RegisteredAt,
    DateTime BirthDay,
    List<TaskDto> Tasks)
{

}
