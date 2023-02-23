using AutoMapper;
using CollectionsAndLinq.BL.Exeptions;
using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services.Abstract;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Users;
using CollectionsAndLinq.DAL.Context;
using CollectionsAndLinq.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CollectionsAndLinq.BL.Services
{
    public class UserService : BaseService, IUserService
    {
        public UserService(IMapper mapper, ProjectDbContext context) : base(mapper, context)
        {
        }

        public async Task<UserDto> AddUser(NewUserDto userDto)
        {
            if(userDto is not null)
            {
                await CheckExistingTeam((int)userDto.TeamId);
            }
            
            var newUser = _mapper.Map<User>(userDto);
            newUser.RegisteredAt = DateTime.Now;

            await _context.AddAsync(newUser);
            await _context.SaveChangesAsync();

            var createdUser = await GetUserByIntenral(newUser.Id);
            var createdUserDto = _mapper.Map<UserDto>(createdUser);

            return createdUserDto;

        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var deleteTask = await CheckExistingUser(id);

            _context.Users.Remove(deleteTask);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<UserDto>> GetAll()
        {
            var users = await _context.Users
                .Include(user => user.Tasks)
                .Include(user => user.Projects)
                .Include(user => user.Team)
                .ToListAsync();

            return _mapper.Map<ICollection<UserDto>>(users);
        }

        public async Task<UserDto> GetById(int id)
        {
            var user = await CheckExistingUser(id);

            return _mapper.Map<UserDto>(user);
        }

        public async System.Threading.Tasks.Task UpdatUser(UpdateUserDto userDto)
        {
            var user = await CheckExistingUser(userDto.Id);

            user.BirthDay = userDto.BirthDay;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UpdateAt = DateTime.UtcNow;
            user.Email = userDto.Email;
            user.TeamId = userDto.TeamId;

            _context.Update(user);
            await _context.SaveChangesAsync();
        }

        private async Task<User> GetUserByIntenral(int id)
        {
            return await _context.Users
                .Include(user => user.Tasks)
                .Include(user => user.Projects)
                .Include(user => user.Team)
                .FirstAsync(user => user.Id == id);
        }

        private async System.Threading.Tasks.Task CheckExistingTeam(int teamId)
        {
            if (!await _context.Teams.AnyAsync(x => x.Id == teamId))
            {
                throw new NotFoundException(nameof(Team), teamId);
            }
        }

        private async Task<User> CheckExistingUser(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user is null)
            {
                throw new NotFoundException(nameof(User), userId);
            }

            return user;
        }
    }
}
