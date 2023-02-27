using AutoMapper;
using CollectionsAndLinq.Common.Models.Projects;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;
using CollectionsAndLinq.DAL.Entities;

namespace CollectionsAndLinq.BL.MappingProFiles
{
    public sealed class TeamProFile : Profile
    {
        public TeamProFile()
        {
            CreateMap<NewTeamDto, Team>();
            CreateMap<UpdateTeamDto, Team>();
            CreateMap<Team, TeamDto>().ReverseMap();
            CreateMap<Team, TeamWithMembersDto>();
        }

    }
}
