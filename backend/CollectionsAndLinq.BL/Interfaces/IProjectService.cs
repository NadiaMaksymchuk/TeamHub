using CollectionsAndLinq.Common.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> GetById(int id);
        Task<ProjectDto> AddProject(NewProjectDto projectDto);
        Task Delete(int id);
        Task UpdateProject(UpdateProjectDto projectDto);
        Task<ICollection<ProjectDto>> GetAll();
    }
}
