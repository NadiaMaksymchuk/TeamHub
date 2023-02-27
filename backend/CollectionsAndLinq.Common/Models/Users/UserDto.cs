using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;

namespace CollectionsAndLinq.Common.Models.Users;

public record UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime BirthDay { get; set; }
}
