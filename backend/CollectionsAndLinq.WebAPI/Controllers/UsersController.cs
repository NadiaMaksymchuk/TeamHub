using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.BL.Services;
using CollectionsAndLinq.Common.Models.Tasks;
using CollectionsAndLinq.Common.Models.Teams;
using CollectionsAndLinq.Common.Models.Users;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<UserDto>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post(NewUserDto newUser)
        {
            return Ok(await _userService.AddUser(newUser));
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateUserDto updateUser)
        {
            await _userService.UpdatUser(updateUser);

            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            return Ok(await _userService.GetById(id));
        }
    }
}
