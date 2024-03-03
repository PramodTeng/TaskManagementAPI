using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Management_API.Auth;
using Task_Management_API.Data.Entity;
using Task_Management_API.Data.Enum;
using Task_Management_API.Exceptions;
using Task_Management_API.Interface;
using Task_Management_API.Model.RequestModel;
using Task_Management_API.Model.ResponseModel;

namespace Task_Management_API.Services
{
    public class TaskDependencyService : ITaskDependencyService
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskDependencyService> _logger;   

        public TaskDependencyService(ApplicationDbContext context, ILogger<TaskDependencyService> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<ActionResult> AddDependencies(int id, IEnumerable<int> dependencyIds)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    throw new NotFoundException("Task Not Found");
                }
                foreach (var depId in dependencyIds)
                {
                    if (HasCircularDependency(id, depId))
                    {
                        return new BadRequestObjectResult("Circular dependency detected.");
                    }
                }

                var newDependencies = new List<int>();
                var existingDependencies = new List<int>();

                foreach (var depId in dependencyIds)
                {
                    if (!_context.TaskDependency.Any(td => td.TaskItemId == id && td.DependentTaskItemId == depId))
                    {
                        newDependencies.Add(depId);
                        task.Dependencies.Add(new TaskDependency { TaskItemId = id, DependentTaskItemId = depId });
                    }
                    else
                    {
                        existingDependencies.Add(depId);
                    }
                }

                await _context.SaveChangesAsync();

                if (newDependencies.Any())
                {
                    return new OkObjectResult(new { message = "New dependencies added successfully" });
                }
                else if (existingDependencies.Any())
                {
                    return new OkObjectResult(new { message = "Dependencies already existed" });
                }
                else
                {
                    return new OkObjectResult(new { message = "No changes were made" });
                }
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while Adding  Dependencies");
                throw;
            }
        }


        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(DataRequestModel dataRequestModel)
        {
            try
            {
                IQueryable<TaskItem> query = _context.Tasks;

                // Filter tasks by status
                if (!string.IsNullOrEmpty(dataRequestModel.status))
                {
                    if (Enum.TryParse(dataRequestModel.status, out StatusType taskStatus))
                    {
                        query = query.Where(t => t.Status == taskStatus);
                    }
                    else
                    {
                        return new BadRequestObjectResult("Invalid status value.");
                    }
                }

                // Filter tasks by due date
                if (dataRequestModel.dueDate != null)
                {
                    query = query.Where(t => t.DueDate >= dataRequestModel.dueDate);
                }

                // Filter tasks by start date
                if (dataRequestModel.startDate != null)
                {
                    query = query.Where(t => t.StartDate >= dataRequestModel.startDate);
                }

                // Sorting
                switch (dataRequestModel.sortBy.ToLower())
                {
                    case "title":
                        query = dataRequestModel.isAscending ? query.OrderBy(t => t.Title) : query.OrderByDescending(t => t.Title);
                        break;
                    case "duedate":
                        query = dataRequestModel.isAscending ? query.OrderBy(t => t.DueDate) : query.OrderByDescending(t => t.DueDate);
                        break;
                    case "startdate":
                        query = dataRequestModel.isAscending ? query.OrderBy(t => t.StartDate) : query.OrderByDescending(t => t.StartDate);
                        break;
                    default:
                        return new BadRequestObjectResult("Invalid sortBy value.");
                }

                // Pagination
                var totalCount = await query.CountAsync();
                var totalPages = totalCount > 0 ? (int)Math.Ceiling(totalCount / (double)dataRequestModel.pageSize) : 1;

                var tasks = await query
                    .Skip((dataRequestModel.pageNumber - 1) * dataRequestModel.pageSize)
                    .Take(dataRequestModel.pageSize)
                    .ToListAsync();

                return new OkObjectResult(new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    PageSize = dataRequestModel.pageSize,
                    PageNumber = dataRequestModel.pageNumber,
                    Tasks = tasks
                });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while fetching tasks");
                throw;

            }
        }



        public async Task<ActionResult> RemoveDependencies(int id, IEnumerable<int> dependencyIds)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    throw new NotFoundException("Task Not Found");
                }

                var dependenciesToRemove = await _context.TaskDependency.Where(td => dependencyIds.Contains(td.DependencyId)).ToListAsync();
                _context.TaskDependency.RemoveRange(dependenciesToRemove);

                await _context.SaveChangesAsync();

                return new OkObjectResult(new { message = "Dependencies removed successfully" });
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while removing dependencies");
                throw;

            }
        }
          
        
        private bool HasCircularDependency(int taskId, int dependentTaskId)
        {
            var visited = new HashSet<int>();
            return DFS(taskId, dependentTaskId, visited);
        }

        private bool DFS(int currentTaskId, int dependentTaskId, HashSet<int> visited)
        {
            if (visited.Contains(currentTaskId))
            {
                return false;
            }

            visited.Add(currentTaskId);

            var currentTask = _context.Tasks.Include(t => t.Dependencies).FirstOrDefault(t => t.Id == currentTaskId);

            if (currentTask == null || currentTask.Dependencies == null)
            {
                return false;
            }

            var dependentTask = _context.Tasks.FirstOrDefault(t => t.Id == dependentTaskId);
            if (dependentTask == null || (dependentTask.Status != StatusType.NotStarted && dependentTask.Status != StatusType.InProgress))
            {
                return false;
            }


            if (currentTask.Dependencies.Any(d => d.DependentTaskItemId == dependentTaskId))
            {
                return true;
            }

            foreach (var dependency in currentTask.Dependencies)
            {
                if (DFS(dependency.DependentTaskItemId, dependentTaskId, visited))
                {
                    return true;
                }
            }

            return false;
        }
    

    public async Task<ActionResult<IEnumerable<TaskResponseModel>>> SearchTasks(string query)
        {

            try
            {
                if (string.IsNullOrEmpty(query))
                {
                    return new BadRequestObjectResult("Search query cannot be empty.");
               
                }

                var matchingTasks = await _context.Tasks
                    .Where(t => t.Title.Contains(query) || t.Description.Contains(query))
                    .ToListAsync();

                return new OkObjectResult(matchingTasks);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while Searching Tasks");
                throw;

            }
        }
    }
}