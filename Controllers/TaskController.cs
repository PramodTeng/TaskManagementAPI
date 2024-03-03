using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Management_API.Auth;
using Task_Management_API.Data.Entity;
using Task_Management_API.Exceptions;
using Task_Management_API.Interface;
using Task_Management_API.Model.RequestModel;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks()
        {
            var tasks = await _taskService.GetTaskAsync();
            return Ok(tasks);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TaskItem>> GetTask(int id)
        {
            var task = await _taskService.GetTasks(id);
            return Ok(task);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TaskItem>> PostTask(TaskRequestModel task)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Model is Invalid");
            }

            await _taskService.CreateTaskAsync(task);
            return Ok(new { message = "Task created successfully", task });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutTask(int id, TaskRequestModel model)
        {
            if (id != model.Id)
            {
                throw new BadRequestException("Id is Invalid");
            }

            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Model is Invalid");
            }
            var updateTask = await _taskService.UpdateTaskAsync(model);

            if (updateTask == null)
            {
                return NotFound();
            }
            return Ok(new { message = "Task Updated Succesfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteAsync(id);
            return Ok(new { message = "Task Deleted Successfully" });
        }
    }
}

