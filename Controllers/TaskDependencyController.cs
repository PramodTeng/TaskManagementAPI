using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Task_Management_API.Interface;
using Task_Management_API.Model.ResponseModel;
using Task_Management_API.Exceptions;
using Task_Management_API.Data.Entity;
using Task_Management_API.Model.RequestModel;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskDependencyController : ControllerBase
    {
        private readonly ITaskDependencyService _taskDependencyService;

        public TaskDependencyController(ITaskDependencyService taskDependencyService)
        {
            _taskDependencyService = taskDependencyService;
        }

        [HttpPost("{id}/AddDependencies")]
        public async Task<IActionResult> AddDependencies(int id, IEnumerable<int> dependencyIds)
        {
             return await _taskDependencyService.AddDependencies(id, dependencyIds);
        }

        [HttpDelete("{id}/RemoveDependencies")]
        public async Task<IActionResult> RemoveDependencies(int id, IEnumerable<int> dependencyIds)
        {
            return await _taskDependencyService.RemoveDependencies(id, dependencyIds);
        }

        [HttpGet("GetTasks")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks([FromQuery]DataRequestModel dataRequestModel)
        {
            return await _taskDependencyService.GetTasks(dataRequestModel);
        }

        [HttpGet("SearchTasks")]
        public async Task<ActionResult<IEnumerable<TaskResponseModel>>> SearchTasks(string query)
        {
           return await _taskDependencyService.SearchTasks(query);
        }
    }
}
