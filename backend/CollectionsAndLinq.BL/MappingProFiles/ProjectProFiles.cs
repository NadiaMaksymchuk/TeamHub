using AutoMapper;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.DAL.Entities;

namespace CollectionsAndLinq.BL.MappingProFiles
{
    public class ProjectProFiles : Profile
    {
        public ProjectProFiles()
        {
            CreateMap<NewProjectDto, Project>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<Project, ProjectDto>().ReverseMap();
            CreateMap<FullProjectDto, Project>()
                .ForMember(project =>project.Autor,ots=>ots.MapFrom(dto => dto.Author))
                .ReverseMap();
        }
    }
}
