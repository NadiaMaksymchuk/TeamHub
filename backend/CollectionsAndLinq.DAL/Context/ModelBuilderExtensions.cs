using Bogus;
using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        private const int UsersCount = 150;
        private const int TeamsCount = 10;
        private const int ProjectCount = 80;
        private const int TasksCount = 200;

        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
              .HasOne(t => t.Team)
              .WithMany(u => u.Members)
              .HasForeignKey(k => k.TeamId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(a => a.Autor)
                .WithMany(p => p.Projects)
                .HasForeignKey(x => x.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entities.Task>()
                .HasOne(p => p.Project)
                .WithMany(t => t.Tasks)
                .HasForeignKey(x => x.ProjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Entities.Task>()
                .HasOne(p => p.Performer)
                .WithMany(x => x.Tasks)
                .HasForeignKey(k => k.PerformerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.Team)
                .WithMany(x => x.Projects)
                .HasForeignKey(k => k.TeamId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            var teams = GenerateRandomTeams();
            var users = GenerateRandomUsers(teams);
            var projects = GenerateProjects(users, teams);
            var tasks = GenerateTasks(projects, users);


            modelBuilder.Entity<Entities.Task>().HasData(tasks);
            modelBuilder.Entity<Project>().HasData(projects);
            modelBuilder.Entity<Team>().HasData(teams);
            modelBuilder.Entity<User>().HasData(users);
        }

        public static ICollection<User> GenerateRandomUsers(ICollection<Team> teams)
        {
            var testUsersFake = new Faker<User>()
                .RuleFor(u => u.Id, f => f.IndexFaker + 1)
                .RuleFor(c => c.FirstName, f => f.Person.FirstName)
                .RuleFor(c => c.LastName, f => f.Person.LastName)
                .RuleFor(c => c.Email, f => f.Person.Email)
                .RuleFor(u => u.BirthDay, f => f.Person.DateOfBirth)
                .RuleFor(u => u.RegisteredAt, (f, o) => f.Date.Between(o.BirthDay.AddYears(16), o.BirthDay.AddYears(40)))
                .RuleFor(u => u.TeamId, f => f.PickRandom(teams).Id)
                .RuleFor(t => t.UpdateAt, (f, o) => f.Date.Between(o.RegisteredAt, f.Date.Past()));

            var generatedUsers = testUsersFake.Generate(UsersCount);

            return generatedUsers;
        }

        public static ICollection<Team> GenerateRandomTeams()
        {
            var testTeamFake = new Faker<Team>()
                .RuleFor(t => t.Id, f => f.IndexFaker + 1)
                .RuleFor(t => t.Name, f => f.Lorem.Word())
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.UpdateAt, (f, o) => f.Date.Between(o.CreatedAt, f.Date.Past()));

            var generatedTeam = testTeamFake.Generate(TeamsCount);

            return generatedTeam;
        }

        public static ICollection<Project> GenerateProjects(ICollection<User> users, ICollection<Team> teams)
        {
            var projectFake = new Faker<Project>()
                .RuleFor(t => t.Id, f => f.IndexFaker + 1)
                .RuleFor(t => t.Name, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentences())
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.Deadline, (f, o) => f.Date.Between(o.CreatedAt, f.Date.Soon()))
                .RuleFor(t => t.AuthorId, f => f.PickRandom(users).Id)
                .RuleFor(t => t.TeamId, f => f.PickRandom(teams).Id)
                .RuleFor(t => t.UpdateAt, (f, o) => f.Date.Between(o.CreatedAt, f.Date.Past()));


            var generatedProject = projectFake.Generate(ProjectCount);

            return generatedProject;
        }

        public static ICollection<Entities.Task> GenerateTasks(ICollection<Project> projects, ICollection<User> users)
        {
            var tasksFake = new Faker<Entities.Task>()
                .RuleFor(t => t.Id, f => f.IndexFaker + 1)
                .RuleFor(t => t.Name, f => f.Lorem.Word())
                .RuleFor(t => t.Description, f => f.Lorem.Sentences())
                .RuleFor(t => t.CreatedAt, f => f.Date.Past())
                .RuleFor(t => t.FinishedAt, (f, o) => f.Date.Between(o.CreatedAt, DateTime.Now).OrNull(f, .20f))
                .RuleFor(t => t.State, f => (TaskState)f.Random.Number(0, 4))
                .RuleFor(t => t.ProjectId, f => f.PickRandom(projects).Id)
                .RuleFor(t => t.PerformerId, f => f.PickRandom(users).Id)
                .RuleFor(t => t.UpdateAt, (f, o) => f.Date.Between(o.CreatedAt, f.Date.Past()));

            var generatedTask = tasksFake.Generate(TasksCount);

            return generatedTask;
        }

    }
}
