using CollectionsAndLinq.BL.Interfaces;
using CollectionsAndLinq.Common.Models.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollectionsAndLinq.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _taskService;

        public TasksController(ITasksService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<TaskDto>>> GetAll()
        {
            return Ok(await _taskService.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<TaskDto>> Add(NewTaskDto taskDto)
        {
            return Ok(await _taskService.Add(taskDto));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(UpdateTaskDto taskDto)
        {
            await _taskService.Update(taskDto);

            return Ok();
        }

        [HttpDelete("id")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetById(int id)
        {
            return Ok(await _taskService.GetById(id));
        }
    }
}
