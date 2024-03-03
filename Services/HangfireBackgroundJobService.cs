using Microsoft.EntityFrameworkCore;
using Task_Management_API.Auth;
using Task_Management_API.Data.Entity;
using Task_Management_API.Data.Enum;
using Task_Management_API.Interface;

namespace Task_Management_API.Services
{
    public class HangfireBackgroundJobService : IHangfireBackgroundJobService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HangfireBackgroundJobService> _logger;
        public HangfireBackgroundJobService(ApplicationDbContext context, ILogger<HangfireBackgroundJobService> logger)
        {
            _context = context;
            _logger = logger;
        }
        //void DeleteTask
        public async Task DailyRecurranceJob()
        {
            try
            {
                var tasks = await _context.Tasks.Where(x => x.Recurrence == RecurrenceType.Daily && x.TaskType == TaskType.Recurring && x.IsCompleted == false).ToListAsync();
                foreach (var task in tasks)
                {
                    if (task.DueDate < DateTime.UtcNow)
                    {
                        task.IsCompleted = true;
                        _context.Tasks.Update(task);
                    }
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while recurring jobs");
                throw;

            }
        }

        public async Task MonthlyRecurranceJob()
        {
            try
            {
                var tasks = await _context.Tasks.Where(x => x.Recurrence == RecurrenceType.Monthly && x.IsCompleted == false).ToListAsync();
                foreach (var task in tasks)
                {
                    if (task.DueDate < DateTime.UtcNow)
                    {
                        task.IsCompleted = true;
                        _context.Tasks.Update(task);
                    }
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while recurring jobs");
                throw;

            }
        }

        public async Task WeeklyRecurranceJob()
        {
            try
            {
                var tasks = await _context.Tasks.Where(x => x.Recurrence == RecurrenceType.Weekly && x.IsCompleted == false).ToListAsync();
                foreach (var task in tasks)
                {
                    if (task.DueDate < DateTime.UtcNow)
                    {
                        task.IsCompleted = true;
                        _context.Tasks.Update(task);
                    }
                }
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                _logger.LogError(ex, "Error ocurred while recurring jobs");
                throw;

            }
        }
    }
}
