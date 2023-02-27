using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.Common.Models.Teams;

public record TeamDto(
    int Id,
    string Name,
    DateTime CreatedAt,
    ICollection<UserDto> Members)
{

}
