using CollectionsAndLinq.Common.Models;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;

namespace CollectionsAndLinq.ClientLogic
{
    internal class DataProcessingClient : BaseClass
    {
        public async Task ShowTasksCountInProjectsByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"GetTasksCountInProjectsByUserId/{userId}");


            var result = Deserializer<Dictionary<string, int>>(await response.Content.ReadAsStringAsync());

            foreach (var task in result)
            {
                Console.WriteLine($"Project info: {task.Key} | Count of tasks: {task.Value}");
            }

            if (result.Count == 0)
            {
                Console.WriteLine("No content");
            }
        }

        public async Task ShowCapitalTasksByUserIdAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"GetCapitalTasksByUserId/{userId}");


            var result = Deserializer<List<TaskDto>>(await response.Content.ReadAsStringAsync());

            foreach (var task in result)
            {
                Console.WriteLine($"Name: {task.Name}\n | Description: {task.Description}\n " +
                    $"| State: {task.State}\n | CreatedAt: {task.CreatedAt}\n");
            }

            if (result.Count == 0)
            {
                Console.WriteLine("No content");
            }

        }

        public async Task ShowProjectsByTeamSizeAsync(int teamSize)
        {
            var response = await _httpClient.GetAsync($"GetProjectsByTeamSize/{teamSize}");

            var result = Deserializer<Dictionary<int, string>>(await response.Content.ReadAsStringAsync());
            foreach (var project in result)
            {
                Console.WriteLine($"Id: {project.Key} | Name: {project.Value}");
            }

            if (result.Count == 0)
            {
                Console.WriteLine("No content");
            }
        }

        public async Task ShowSortedTeamByMembersWithYearAsync(int year)
        {
            var response = await _httpClient.GetAsync($"GetSortedTeamByMembersWithYear/{year}");

            var result = Deserializer<List<TeamWithMembersDto>>(await response.Content.ReadAsStringAsync());

            foreach (var team in result)
            {
                Console.WriteLine($"Name: {team.Name} | Members: ");

                foreach (var member in team.Members)
                {
                    Console.WriteLine($"Name: {member.FirstName}  {member.LastName} | BirthDay: {member.BirthDay} ");
                }
            }

            if (result.Count == 0)
            {
                Console.WriteLine("No content");
            }
        }

        public async Task ShowSortedUsersWithSortedTasksAsync()
        {
            var response = await _httpClient.GetAsync($"GetSortedUsersWithSortedTasks");

            var result = Deserializer<List<UserWithTasksDto>>(await response.Content.ReadAsStringAsync());

            foreach (var user in result)
            {
                Console.WriteLine($"Name: {user.FirstName}  {user.LastName} | BirthDay: {user.BirthDay} ");

                Console.WriteLine("Tasks");

                foreach (var task in user.Tasks)
                {
                    Console.WriteLine($"Name: {task.Name} | Description: {task.Description}\n | State: {task.State} | CreatedAt: {task.CreatedAt}");
                }

                Console.WriteLine();
            }

            if (result.Count == 0)
            {
                Console.WriteLine("No content");
            }
        }

        public async Task ShowUserInfoAsync(int userId)
        {
            var response = await _httpClient.GetAsync($"GetUserInfo/{userId}");

            var result = Deserializer<UserInfoDto>(await response.Content.ReadAsStringAsync());

            Console.WriteLine($"Name: {result.User.FirstName}  {result.User.LastName} " +
                $"| BirthDay: {result.User.BirthDay}\n " +
                $"| Last project tasks count: {result.LastProjectTasksCount} " +
                $"| Not finished or canceled tasks count: {result.NotFinishedOrCanceledTasksCount} \n" +
                $"| Last project name: {result.LastProject?.Name ?? string.Empty} " +
                $"| Longest task name: {result.LongestTask?.Name ?? string.Empty}");

            if (result is null)
            {
                Console.WriteLine("No content");
            }
        }

        public async Task ShowProjectsInfoAsync()
        {
            var response = await _httpClient.GetAsync($"GetProjectsInfo");

            var result = Deserializer<List<ProjectInfoDto>>(await response.Content.ReadAsStringAsync());

            foreach (var project in result)
            {
                Console.WriteLine($"\nId: {project.Project.Id} | Name: {project.Project.Name} " +
                    $"| Team members count: {project.TeamMembersCount}\n | Shortest task name: {project.ShortestTaskByName?.Name ?? string.Empty}\n" +
                    $" | Longest task by description: {project.LongestTaskByDescription?.Name ?? string.Empty}\n");
            }

            if (result is null)
            {
                Console.WriteLine("No content");
            }
        }

        public async Task ShowSortedFilteredPageOfProjectsAsync(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
        {
            var response = await _httpClient.GetAsync($"GetSortedFilteredPageOfProjects?" +
                $"PageModel.PageSize={pageModel.PageSize}&" +
                $"PageModel.PageNumber={pageModel.PageNumber}&" +
                $"FilterModel.Name={filterModel.Name}&" +
                $"FilterModel.Description={filterModel.Description}&" +
                $"FilterModel.AutorFirstName={filterModel.AutorFirstName}&" +
                $"FilterModel.AutorLastName={filterModel.AutorLastName}&" +
                $"FilterModel.TeamName={filterModel.TeamName}&" +
                $"SortingModel.Property={(int)sortingModel.Property}&" +
                $"SortingModel.Order={(int)sortingModel.Order}");

            var result = Deserializer<PagedList<FullProjectDto>>(await response.Content.ReadAsStringAsync());

            Console.WriteLine(response.ReasonPhrase);

            Console.WriteLine($"Total count: {result.TotalCount}\n ");

            foreach (var project in result.Items)
            {
                Console.WriteLine($"Project name: {project.Name}\n | Project deadline: {project.Deadline}" +
                    $" | Description: {project.Description}\n | Created at: {project.CreatedAt} | Author name: {project.Author.FirstName} {project.Author.LastName}\n " +
                    $"| Team name: {project.Team.Name} | Task count: {project.Tasks.Count}\n");
            }
        }
    }
}
