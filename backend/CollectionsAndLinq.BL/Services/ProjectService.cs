using AutoMapper;
using CollectionsAndLinq.BL.Exeptions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace CollectionsAndLinq.BL.Services
{
    public class ProjectService : BaseService, IProjectService
    {
        public ProjectService(IMapper mapper, ProjectDbContext context) : base(mapper, context)
        {
        }

        public async Task<ProjectDto> AddProject(NewProjectDto projectDto)
        {
            await CheckExistingAuthorAndTeam(projectDto.AuthorId, projectDto.TeamId);

            var newProject = _mapper.Map<Project>(projectDto);
            newProject.CreatedAt = DateTime.Now;

            _context.Projects.Add(newProject);
            await _context.SaveChangesAsync();

            var createdProject = await GetProjectByInternal(newProject.Id);

            var createdProjectDto = _mapper.Map<ProjectDto>(createdProject);

            return createdProjectDto;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var deleteProject = await CheckExistingProject(id);

            _context.Projects.Remove(deleteProject);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ProjectDto>> GetAll()
        {
            var projects = await _context.Projects
                .Include(project => project.Autor)
                .Include(project => project.Team)
                .Include(project => project.Tasks).ToListAsync();

            return _mapper.Map<ICollection<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetById(int id)
        {
            var project = await CheckExistingProject(id);

            return _mapper.Map<ProjectDto>(project);
        }

        public async System.Threading.Tasks.Task UpdateProject(UpdateProjectDto projectDto)
        {
            var project = await CheckExistingProject(projectDto.Id);

            await CheckExistingAuthorAndTeam(projectDto.AuthorId, projectDto.TeamId);

            project.AuthorId = projectDto.AuthorId;
            project.Deadline = projectDto.Deadline;
            project.Name = projectDto.Name;
            project.Description = projectDto.Description;
            project.UpdateAt = DateTime.Now;
            project.TeamId = projectDto.TeamId;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();  
        }

        private async Task<Project> GetProjectByInternal(int id)
        {
            return await _context.Projects
                .Include(project => project.Autor)
                .Include(project => project.Team)
                .Include(project => project.Tasks)
                .FirstAsync(project => project.Id == id);
        }

        private async System.Threading.Tasks.Task CheckExistingAuthorAndTeam(int authorId, int teamId)
        {
            if (!await _context.Users.AnyAsync(x => x.Id == authorId))
            {
                throw new NotFoundException(nameof(User), authorId);
            }

            if (!await _context.Teams.AnyAsync(x => x.Id == teamId))
            {
                throw new NotFoundException(nameof(Team), teamId);
            }
        }

        private async Task<Project> CheckExistingProject(int projectId)
        {
            var project = await _context.Projects
                                        .Include(project => project.Autor)
                                        .Include(project => project.Team)
                                        .FirstOrDefaultAsync(x => x.Id == projectId);

            if (project is null)
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            return project;
        }
    }
}
