using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.Common.Models.Tasks;

public record TaskWithPerfomerDto
{
    public TaskWithPerfomerDto()
    {

    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string State { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
    public UserDto Perfomer { get; set; }
}
