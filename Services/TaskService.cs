using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NPOI.OpenXmlFormats;
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
    public class TaskService : ITaskService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<TaskService> _logger;
        public TaskService(ApplicationDbContext context, ILogger<TaskService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<TaskItem> CreateTaskAsync(TaskRequestModel model)
        {
            try
            {
                var entity = new TaskItem
                {
                  
                    Title = model.Title,
                    Description = model.Description,
                    DueDate = model.DueDate,
                    StartDate = model.StartDate,
                    Status = model.Status,
                    TaskType = model.TaskType,
                    Recurrence = model.Recurrence,
                    IsCompleted = false
                };

                if (entity == null)
                {
                    throw new NotFoundException("Entity is Empty");
                }

                await _context.Tasks.AddAsync(entity);
               await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while Creating Tasks");
                throw;

            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);

                if (task == null)
                {
                    throw new NotFoundException("Task Not Found");
                }
                try
                {
                    _context.Tasks.Remove(task);
                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to delete task", ex);

                }

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while deleting tasks");
                throw;

            }
        }

        public async Task<IEnumerable<TaskResponseModel>> GetTaskAsync()
        {
            try
            {
                var tasks = await _context.Tasks.ToListAsync();
                if (tasks == null)
                {
                    throw new NotFoundException("Task Not Found");
                }

                var response = new List<TaskResponseModel>();
                foreach (var task in tasks)
                {
                    var model = new TaskResponseModel
                    {
                        Id = task.Id,
                        Title = task.Title,
                        Description = task.Description,
                        DueDate = task.DueDate,
                        StartDate = task.StartDate,
                        Status = task.Status,
                        Recurrence = task.Recurrence,
                        TaskType = task.TaskType,
                        IsCompleted = task.IsCompleted
                    };
                    response.Add(model);
                }
                return response;
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while retrieving tasks");
                throw;

            }
        }

        public async Task<TaskResponseModel> GetTasks(int id)
        {
            try
            {
                var task = await _context.Tasks.FindAsync(id);
                if (task == null)
                {
                    throw new NotFoundException("Task Not Found");
                }
                var response = new TaskResponseModel
                {
                    Id = task.Id,
                    Title = task.Title,
                    Description = task.Description,
                    DueDate = task.DueDate,
                    StartDate = task.StartDate,
                    Status = task.Status,
                    Recurrence = task.Recurrence,
                    TaskType = task.TaskType,
                    IsCompleted = task.IsCompleted
                };

                return response;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while retrieving task by Id");
                throw;

            }
        }

        public async Task<TaskItem> UpdateTaskAsync(TaskRequestModel model)
        {
            try
            {
                var existingTask = await _context.Tasks.FindAsync(model.Id);

                if (existingTask == null)
                {
                    throw new Exception("Task Not Found");
                }

                existingTask.Title = model.Title;
                existingTask.Description = model.Description;
                existingTask.DueDate = model.DueDate;
                existingTask.StartDate = model.StartDate;
                existingTask.Status = model.Status;



                if (model.TaskType == TaskType.Recurring)
                {
                    existingTask.Recurrence = model.Recurrence;
                }
                else
                {
                    existingTask.Recurrence = RecurrenceType.None;
                }

                await _context.SaveChangesAsync();

                return existingTask;

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while updating tasks");
                throw;

            }
        }

    }
}
