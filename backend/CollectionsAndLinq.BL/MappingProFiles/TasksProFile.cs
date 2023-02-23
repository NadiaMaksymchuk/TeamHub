using AutoMapper;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Users;
using CollectionsAndLinq.DAL.Entities;

namespace CollectionsAndLinq.BL.MappingProFiles
{
    public class TasksProFile : Profile
    {
        public TasksProFile()
        {
            CreateMap<NewTaskDto, DAL.Entities.Task>();
            CreateMap<UpdateTaskDto, DAL.Entities.Task>();
            CreateMap<DAL.Entities.Task, TaskDto>().ReverseMap();
            CreateMap<DAL.Entities.Task, TaskWithPerfomerDto>();
        }
    }
}
