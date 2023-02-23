using CollectionsAndLinq.Common.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetById(int id);
        Task<UserDto> AddUser(NewUserDto userDto);
        Task Delete(int id);
        Task UpdatUser(UpdateUserDto userDto);
        Task<ICollection<UserDto>> GetAll();
    }
}
