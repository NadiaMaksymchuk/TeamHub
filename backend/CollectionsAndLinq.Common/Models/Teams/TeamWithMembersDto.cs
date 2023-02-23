using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.Common.Models.Teams;

public record TeamWithMembersDto(
    int Id,
    string Name,
    List<UserDto> Members)
{

}
