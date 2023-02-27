using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface ITasksService
    {
        Task<TaskDto> Add(NewTaskDto task);
        Task Delete(int id);
        Task Update(UpdateTaskDto taskDto);
        Task<TaskDto> GetById(int id);
        Task<ICollection<TaskDto>> GetAll();
    }
}
