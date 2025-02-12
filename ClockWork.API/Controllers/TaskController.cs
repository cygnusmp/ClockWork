using ClockWork.Domain.Entities;
using ClockWork.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ClockWork.API.Controllers
{
    /// <summary>
    /// Controller for managing tasks.
    /// </summary>
    [Route("api/task")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskController"/> class.
        /// </summary>
        /// <param name="taskRepository">The task repository.</param>
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [SwaggerOperation(Summary = "Gets all tasks.")]
        [HttpGet]
        public IActionResult Get()
        {
            var taska = _taskRepository.GetAll();
            return Ok(taska);
        }

        [SwaggerOperation(Summary = "Gets a task by its identifier.")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var task = _taskRepository.GetById(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [SwaggerOperation(Summary = "Creates a new task.")]
        [HttpPost]
        public IActionResult Create(TaskEntity task)
        {
            var newTask = _taskRepository.Add(task);
            return CreatedAtAction(nameof(Get), new { id = newTask.Id }, newTask);
        }

        [SwaggerOperation(Summary = "Updates a existing task.")]
        [HttpPut]
        public IActionResult Update(TaskEntity task)
        {
            _taskRepository.Update(task);
            return NoContent();
        }

        [SwaggerOperation(Summary = "Deletes a task.")]
        [HttpDelete]
        public IActionResult Delete(TaskEntity task)
        {
            _taskRepository.Delete(task);
            return NoContent();
        }
    }
}
