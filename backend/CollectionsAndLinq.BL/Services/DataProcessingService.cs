using AutoMapper;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.Common.Models;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services;

public class DataProcessingService : BaseService, IDataProcessingService
{
    public DataProcessingService(IMapper mapper, ProjectDbContext context) : base(mapper, context)
    {
    }

    public ICollection<TaskDto> GetCapitalTasksByUserId(int userId)
    {
        return _mapper.Map<ICollection<TaskDto>>(_context.Tasks.Include(x => x.Performer).ToList()
            .Where(x => x.PerformerId == userId && char.IsUpper(x.Name, 0))
            .Select(x => new DAL.Entities.Task
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                State = x.State,
                CreatedAt = x.CreatedAt,
                FinishedAt = x.FinishedAt,
                Performer = x.Performer,
            }).ToList());
    }

    public async Task<Dictionary<int, string>> GetProjectsByTeamSize(int teamSize)
    {
        return await _context.Projects
            .Where(x => x.Team.Members.Count() > teamSize)
            .ToDictionaryAsync(x => x.Id, x => x.Name);
    }

    public async Task<List<ProjectInfoDto>> GetProjectsInfo()
    {
        return await _context.Projects.Select(x => new ProjectInfoDto(
            _mapper.Map<ProjectDto>(x),

            _mapper.Map<TaskDto>(x.Tasks.OrderByDescending(t => t.Description.Length).FirstOrDefault()),

            _mapper.Map<TaskDto>(x.Tasks
            .OrderBy(t => t.Name.Length)
            .FirstOrDefault()),

            (x.Description.Length > 20 || x.Tasks.Count < 3) ? x.Team.Members.Count : null))
            .ToListAsync();
    }

    public async Task<PagedList<FullProjectDto>> GetSortedFilteredPageOfProjects(PageModel pageModel, FilterModel filterModel, SortingModel sortingModel)
    {
        var fullProject = _mapper.Map<List<FullProjectDto>>(await _context.Projects
            .Include(project => project.Autor)
            .Include(project => project.Team)
            .Include(project => project.Tasks)
            .ToListAsync());

        var result = new PagedList<FullProjectDto>(fullProject, fullProject.Count);

        if (pageModel is not null)
        {
            var fullProjectCache = fullProject;

            if (filterModel is not null)
            {
                fullProjectCache = Filter(fullProjectCache, filterModel);
            }

            if (sortingModel is not null)
            {
                fullProjectCache = Sorting(fullProjectCache, sortingModel);
            }

            PagedList<FullProjectDto> resultPage = new PagedList<FullProjectDto>(fullProjectCache, fullProjectCache.Count);

            if (fullProjectCache.Count >= pageModel.PageSize * pageModel.PageNumber)
            {
                return new PagedList<FullProjectDto>(fullProjectCache
                .GetRange(pageModel.PageSize * (pageModel.PageNumber - 1), 
                pageModel.PageSize), fullProjectCache.Count);
            }
            if (fullProjectCache.Count > pageModel.PageSize * pageModel.PageNumber)
            {
                return new PagedList<FullProjectDto>(fullProjectCache
                .GetRange(pageModel.PageSize * (pageModel.PageNumber - 1), 
                fullProjectCache.Count - pageModel.PageSize * (pageModel.PageNumber - 1)), fullProjectCache.Count);
            }

            return resultPage;
        }

        if (filterModel is not null)
        {
            var fullProjectCache = fullProject;

            fullProjectCache = Filter(fullProjectCache, filterModel);

            if (sortingModel is not null)
            {
                fullProjectCache = Sorting(fullProjectCache, sortingModel);
            }

            var resultFilter = new PagedList<FullProjectDto>(fullProjectCache, fullProjectCache.Count);

            return resultFilter;
        }

        if (sortingModel is not null)
        {
            var fullProjectCache = fullProject;

            if (sortingModel is not null)
            {
                fullProjectCache = Sorting(fullProjectCache, sortingModel);
            }

            var resultSorting = new PagedList<FullProjectDto>(fullProjectCache, fullProjectCache.Count);

            return resultSorting;
        }

        return result;
    }

    public async Task<ICollection<TeamWithMembersDto>> GetSortedTeamByMembersWithYear(int year)
    {
        return _mapper.Map<ICollection<TeamWithMembersDto>>(await _context.Teams
            .Where(x => x.Members.Count != 0 && x.Members
            .All(y => y.BirthDay.Year < year))
            .Select(x => new Team
            {
                Id = x.Id,
                Name = x.Name,
                CreatedAt = x.CreatedAt,
                Members = x.Members.OrderByDescending(m => m.RegisteredAt).ToList(),
            })
            .OrderBy(x => x.Name)
            .ToListAsync());
    }

    public async Task<ICollection<UserWithTasksDto>> GetSortedUsersWithSortedTasks()
    {
        return _mapper.Map<ICollection<UserWithTasksDto>>(await _context
            .Users.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                RegisteredAt = x.RegisteredAt,
                BirthDay = x.BirthDay,
                Tasks = x.Tasks.OrderByDescending(t => t.Name.Length).ToList()
            })
            .OrderBy(x => x.FirstName)
            .ToListAsync());
    }

    public async Task<Dictionary<string, int>> GetTasksCountInProjectsByUserId(int userId)
    {
        return await _context.Projects
            .Where(x => x.AuthorId == userId)
            .ToDictionaryAsync(x => $"{x.Id}: {x.Name}", x => x.Tasks.Count);
    }

    public async Task<UserInfoDto> GetUserInfo(int userId)
    {
        return await _context.Users
            .Where(x => x.Id == userId).Select(x => new UserInfoDto(
            _mapper.Map<UserDto>(x),

            _mapper.Map<ProjectDto>(
            x.Tasks.Select(p => p.Project)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefault()),

            x.Tasks.Select(p => p.Project)
            .OrderByDescending(p => p.CreatedAt)
            .FirstOrDefault().Tasks.Count(),

            x.Tasks.Select(p => p.Project)
            .SelectMany(t => t.Tasks)
            .Where(t => t.State == TaskState.ToDo
            || t.State == TaskState.InProgress || t.State == TaskState.Canceled).Count(),
            
            _mapper.Map<TaskDto>(x.Projects
            .SelectMany(t => t.Tasks).FirstOrDefault())))
           .FirstOrDefaultAsync();
    }

    private List<FullProjectDto> Sorting(List<FullProjectDto> fullProjectCache, SortingModel sortingModel)
    {
        if (sortingModel.Order == SortingOrder.Descending)
        {
            switch (sortingModel.Property)
            {
                case SortingProperty.Name:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Name).ToList();
                    break;
                case SortingProperty.TasksCount:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Tasks.Count).ToList();
                    break;
                case SortingProperty.AutorLastName:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Author.LastName).ToList();
                    break;
                case SortingProperty.AutorFirstName:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Author.FirstName).ToList();
                    break;
                case SortingProperty.Description:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Description).ToList();
                    break;
                case SortingProperty.CteatedAt:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.CreatedAt).ToList();
                    break;
                case SortingProperty.Deadline:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Deadline).ToList();
                    break;
                case SortingProperty.TeamName:
                    fullProjectCache = fullProjectCache.OrderByDescending(x => x.Team.Name).ToList();
                    break;
            }
        }

        if (sortingModel.Order == SortingOrder.Ascending)
        {
            switch (sortingModel.Property)
            {
                case SortingProperty.Name:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Name).ToList();
                    break;
                case SortingProperty.TasksCount:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Tasks.Count).ToList();
                    break;
                case SortingProperty.AutorLastName:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Author.LastName).ToList();
                    break;
                case SortingProperty.AutorFirstName:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Author.FirstName).ToList();
                    break;
                case SortingProperty.Description:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Description).ToList();
                    break;
                case SortingProperty.CteatedAt:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.CreatedAt).ToList();
                    break;
                case SortingProperty.Deadline:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Deadline).ToList();
                    break;
                case SortingProperty.TeamName:
                    fullProjectCache = fullProjectCache.OrderBy(x => x.Team.Name).ToList();
                    break;
            }
        }

        return fullProjectCache;
    }

    private List<FullProjectDto> Filter(List<FullProjectDto> fullProjectCache, FilterModel filterModel)
    {
        return fullProjectCache.Where(x => x.Name.Contains(filterModel.Name ?? string.Empty)
            && x.Description.Contains(filterModel.Description ?? string.Empty)
            && x.Author.FirstName.Contains(filterModel.AutorFirstName ?? string.Empty)
            && x.Author.LastName.Contains(filterModel.AutorLastName ?? string.Empty)
            && x.Team.Name.Contains(filterModel.TeamName ?? string.Empty)).ToList();
    }
}
