namespace CollectionsAndLinq.DAL.Entities;

public record User
{ 
    public User()
    {
        Tasks = new List<Task>();
        Projects = new List<Project>();
    }

    public int Id { get; set; }
    public int? TeamId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public DateTime BirthDay { get; set; }
    public Team Team { get; set; }
    public ICollection<Task> Tasks { get; set; }
    public ICollection<Project> Projects { get; set; }
    
}
