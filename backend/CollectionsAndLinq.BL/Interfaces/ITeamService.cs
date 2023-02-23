using CollectionsAndLinq.Common.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDto> AddTeam(NewTeamDto team);
        Task DeleteTeam(int id);
        Task UpdateTeam(UpdateTeamDto teamDto);
        Task<TeamDto> GetById(int id);
        Task<ICollection<TeamDto>> GetAll();
    }
}
