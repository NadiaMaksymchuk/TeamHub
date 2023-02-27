namespace CollectionsAndLinq.DAL.Entities;

public record Project
{
    public Project()
    {
        Tasks = new List<Task>();
    }

    public int Id { get; set; }
    public int AuthorId { get; set; }
    public int TeamId { get; set; } 
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime Deadline { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public User Autor { get; set; }
    public Team Team { get; set; }
}
