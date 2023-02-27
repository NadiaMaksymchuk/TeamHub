using AutoMapper;
using CollectionsAndLinq.BL.Exeptions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CollectionsAndLinq.BL.Services
{
    public class TaskService : BaseService, ITasksService
    {
        public TaskService(IMapper mapper, ProjectDbContext context) : base(mapper, context) { }

        public async Task<TaskDto> Add(NewTaskDto task)
        {
            await CheckExistingUserAndPerformer(task.ProjectId, task.PerformerId);

            var newTask = _mapper.Map<DAL.Entities.Task>(task);
            newTask.CreatedAt = DateTime.UtcNow;

            await _context.Tasks.AddAsync(newTask);
            await _context.SaveChangesAsync();

            var createdTask = await GetTaskByInternal(newTask.Id);

            var createdTaskDto = _mapper.Map<TaskDto>(createdTask);

            return createdTaskDto;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var deleteTask = await CheckExistingTask(id);

            _context.Tasks.Remove(deleteTask);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TaskDto>> GetAll()
        {
            var tasks = await _context.Tasks
                .Include(task => task.Performer)
                .Include(tasks => tasks.Project)
                .ToListAsync();

            return _mapper.Map<ICollection<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetById(int id)
        {
            await CheckExistingTask(id);

            var task = await GetTaskByInternal(id);

            return _mapper.Map<TaskDto>(task);
        }

        public async System.Threading.Tasks.Task Update(UpdateTaskDto taskDto)
        {
            var task = await CheckExistingTask(taskDto.Id);

            await CheckExistingUserAndPerformer(taskDto.ProjectId, taskDto.PerformerId);

            TaskState updatedTaskState;

            if (!Enum.TryParse(taskDto.State, out updatedTaskState))
            {
                throw new InvalidCastException(taskDto.State);
            }

            task.UpdateAt = DateTime.UtcNow;
            task.Description = taskDto.Description;
            task.State = updatedTaskState;
            task.FinishedAt = taskDto.FinishedAt;
            task.ProjectId = taskDto.ProjectId;
            task.PerformerId = taskDto.PerformerId;
            task.Name = taskDto.Name;

            _context.Tasks.Update(task);
            await _context.SaveChangesAsync();
        }

        private async Task<DAL.Entities.Task> GetTaskByInternal(int id)
        {
            return await _context.Tasks.
                Include(task => task.Performer)
                .Include(task => task.Project)
                .FirstAsync(task => task.Id == id);
        }

        private async System.Threading.Tasks.Task CheckExistingUserAndPerformer(int projectId, int userId)
        {
            if (!await _context.Projects.AnyAsync(x => x.Id == projectId))
            {
                throw new NotFoundException(nameof(Project), projectId);
            }

            if (!await _context.Users.AnyAsync(x => x.Id == userId))
            {
                throw new NotFoundException(nameof(User), userId);
            }
        }

        private async System.Threading.Tasks.Task<DAL.Entities.Task> CheckExistingTask(int taskId)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.Id == taskId);

            if (task is null)
            {
                throw new NotFoundException(nameof(DAL.Entities.Task), taskId);
            }

            return task;
        }
    }
}
