namespace CollectionsAndLinq.DAL.Entities;

public record Team
{
    public Team()
    {
        Members = new List<User>();
        Projects = new List<Project>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<User> Members { get; set; }
    public virtual ICollection<Project> Projects { get; set; }
}
