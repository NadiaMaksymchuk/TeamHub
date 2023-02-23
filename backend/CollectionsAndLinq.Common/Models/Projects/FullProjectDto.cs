using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.Common.Models.Projects;

public record FullProjectDto
{
    public FullProjectDto()
    {
        Tasks = new List<TaskWithPerfomerDto>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime Deadline { get; set; }
    public UserDto Author { get; set; }
    public TeamDto Team { get; set; }
    public ICollection<TaskWithPerfomerDto> Tasks { get; set; }
}
