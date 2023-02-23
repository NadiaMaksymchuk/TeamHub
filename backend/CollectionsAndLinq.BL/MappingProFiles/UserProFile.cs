using AutoMapper;
using CollectionsAndLinq.Common.Models.Users;
using CollectionsAndLinq.DAL.Entities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CollectionsAndLinq.BL.MappingProFiles
{
    public class UserProFile : Profile
    {
        public UserProFile()
        {
            CreateMap<NewUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<User, UserWithTasksDto>();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
