using CollectionsAndLinq.ClientLogic;
using CollectionsAndLinq.Common.Models.Projects;

namespace CoolParking
{
    internal static class Menus
    {
        static readonly DataProcessingClient _dataProcessingClient;
        public static bool Work = true;

        static Menus()
        {
            _dataProcessingClient = new DataProcessingClient();
        }

        public async static Task FirstMenu()
        {
            short curItem = 0;
            string[] menuItems = new string[] {
                "Get tasks count in projects by user id",
                "Get capital tasks by user id",
                "Get projects by team size",
                "Get sorted team by members with year",
                "Get sorted users with sorted tasks",
                "Get user info",
                "Get projects info",
                "Get sorted filtered page of projects",
                "Exit"
            };

            switch (MenuLogic.Menu(curItem, menuItems))
            {
                case 0:
                    await ShowTasksCountInProjectsByUserIdAsync();
                    break;
                case 1:
                    await ShowCapitalTasksByUserIdAsync();
                    break;
                case 2:
                    await ShowProjectsByTeamSizeAsync();
                    break;
                case 3:
                    await ShowSortedTeamByMembersWithYearAsync();
                    break;
                case 4:
                    await ShowSortedUsersWithSortedTasksAsync();
                    break;
                case 5:
                    await ShowUserInfoAsync();
                    break;
                case 6:
                    await ShowProjectsInfoAsync();
                    break;
                case 7:
                    await ShowSortedFilteredPageOfProjectsAsync();
                    break;
                case 8:
                    Work = false;
                    return;
            }
        }

        public static async Task ShowTasksCountInProjectsByUserIdAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get tasks count in projects by user id", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter user id");
                    int userId;

                    if (int.TryParse(Console.ReadLine(), out userId))
                    {
                        await _dataProcessingClient.ShowTasksCountInProjectsByUserIdAsync(userId);
                    }
                    else
                    {
                        Console.WriteLine("Enter valid number");
                    }
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }

        public static async Task ShowCapitalTasksByUserIdAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get capital tasks by user id", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter user id");
                    int id;

                    if(int.TryParse(Console.ReadLine(), out id))
                    {
                        await _dataProcessingClient.ShowCapitalTasksByUserIdAsync(id);
                    }
                    else
                    {
                        Console.WriteLine("Enter valid number");
                    }    
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }

        public static async Task ShowProjectsByTeamSizeAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get projects by team size", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter team size");
                    int size;

                    if(int.TryParse(Console.ReadLine(), out size))
                    {
                        await _dataProcessingClient.ShowProjectsByTeamSizeAsync(size);
                    }
                    else
                    {
                        Console.WriteLine("Enter valid number");
                    }
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }

        public static async Task ShowSortedTeamByMembersWithYearAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get sorted team by members with year", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter year");
                    int year;

                    if(int.TryParse(Console.ReadLine(), out year))
                    {
                        await _dataProcessingClient.ShowSortedTeamByMembersWithYearAsync(year);
                    }
                    else
                    {
                        Console.WriteLine("Enter valid number");
                    }
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }

        public static async Task ShowSortedUsersWithSortedTasksAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get sorted users with sorted tasks", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    await _dataProcessingClient.ShowSortedUsersWithSortedTasksAsync();
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }

        public static async Task ShowUserInfoAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get user info", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter user id");
                    int userId;

                    if(int.TryParse(Console.ReadLine(), out userId))
                    {
                        await _dataProcessingClient.ShowUserInfoAsync(userId);
                    }
                    else
                    {
                        Console.WriteLine("Enter valid number");
                    }

                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }

        public static async Task ShowProjectsInfoAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get projects info", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    await _dataProcessingClient.ShowProjectsInfoAsync();
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }


        public static async Task ShowSortedFilteredPageOfProjectsAsync()
        {
            short curItem = 0;
            string[] menuSelect = { "Get sorted filtered page of projects", "Return to main menu " };

            switch (MenuLogic.Menu(curItem, menuSelect))
            {
                case 0:
                    Console.WriteLine("Enter page size");
                    int pageSize = int.Parse(Console.ReadLine());
                    Console.WriteLine("Enter page index");
                    int pageIndex = int.Parse(Console.ReadLine());
                    PageModel pageModel = new PageModel(pageSize, pageIndex);

                    Console.WriteLine($"Filter for name:");
                    string name = Console.ReadLine();
                    Console.WriteLine($"Filter for description:");
                    string description = Console.ReadLine();
                    Console.WriteLine($"Filter for author first name:");
                    string authorFirstName = Console.ReadLine();
                    Console.WriteLine($"Filter for author last name:");
                    string authorLastName = Console.ReadLine();
                    Console.WriteLine($"Filter for team name:");
                    string teamName = Console.ReadLine();
                    FilterModel filterModel = new FilterModel(Name: name, Description: description, AutorFirstName: authorFirstName, AutorLastName: authorLastName, TeamName: teamName);

                    Console.WriteLine($"Order by desending? (y/n)");
                    string choose = Console.ReadLine();

                    SortingOrder sortingOrder;

                    if (choose.ToLower() == "y" || choose.ToLower() == "yes")
                    {
                        sortingOrder = SortingOrder.Descending;
                    }
                    else
                    {
                        sortingOrder = SortingOrder.Ascending;
                    }

                    SortingProperty sortingProperty = SortingProperty.Name;
                    Console.WriteLine($"Sorting by: (Name, Description, Deadline, Created at, Tasks count, Author firstname, Author lastname, Team name)");
                    string property = Console.ReadLine();
                    switch (property.ToLower())
                    {
                        case "name":
                            sortingProperty = SortingProperty.Name;
                            break;
                        case "description":
                            sortingProperty = SortingProperty.Description;
                            break;
                        case "deadline":
                            sortingProperty = SortingProperty.Deadline;
                            break;
                        case "created at":
                            sortingProperty = SortingProperty.CteatedAt;
                            break;
                        case "tasks count":
                            sortingProperty = SortingProperty.TasksCount;
                            break;
                        case "author firstname":
                            sortingProperty = SortingProperty.AutorFirstName;
                            break;
                        case "autor lastname":
                            sortingProperty = SortingProperty.AutorLastName;
                            break;
                        case "team name":
                            sortingProperty = SortingProperty.TeamName;
                            break;
                        default:
                            Console.WriteLine("Invalid sorting property");
                            break;
                    }
                    SortingModel sortingModel = new SortingModel(sortingProperty, sortingOrder);
                    await _dataProcessingClient.ShowSortedFilteredPageOfProjectsAsync(pageModel, filterModel, sortingModel);
                    Console.ReadKey();
                    break;
                case 1:
                    await FirstMenu();
                    break;
            }
        }
    }
}
