using AutoMapper;
using CollectionsAndLinq.BL.Exeptions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class TeamService : BaseService, ITeamService
    {
        public TeamService(IMapper mapper, ProjectDbContext context) : base(mapper, context)
        {
        }

        public async Task<TeamDto> AddTeam(NewTeamDto team)
        {
            var newTeam = _mapper.Map<Team>(team);
            newTeam.CreatedAt = DateTime.Now;

            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();

            var createdTeam = await GetTeamByInternal(newTeam.Id);

            var createdTeamDto = _mapper.Map<TeamDto>(createdTeam);

            return createdTeamDto;
        }

        public async System.Threading.Tasks.Task DeleteTeam(int id)
        {
            var deleteTeam = await CheckExistingTeam(id);

            _context.Teams.Remove(deleteTeam);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<TeamDto>> GetAll()
        {
            var teams = await _context.Teams
                .Include(team => team.Members)
                .Include(team => team.Projects)
                .ToListAsync();

            return _mapper.Map<ICollection<TeamDto>>(teams);
        }

        public async Task<TeamDto> GetById(int id)
        {
            var team = await CheckExistingTeam(id);

            return _mapper.Map<TeamDto>(team);
        }

        public async System.Threading.Tasks.Task UpdateTeam(UpdateTeamDto teamDto)
        {
            var team = await CheckExistingTeam(teamDto.Id);

            team.Name = teamDto.Name;
            team.UpdateAt = DateTime.Now;
            _context.Teams.Update(team);

            await _context.SaveChangesAsync();
        }

        private async Task<Team> GetTeamByInternal(int id)
        {
            return await _context.Teams.
                Include(team => team.Members)
                .Include(team => team.Projects)
                .FirstAsync(team => team.Id == id);
        }

        private async Task<Team> CheckExistingTeam(int teamId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == teamId);

            if (team is null)
            {
                throw new NotFoundException(nameof(Team), teamId);
            }

            return team;
        }
    }
}
