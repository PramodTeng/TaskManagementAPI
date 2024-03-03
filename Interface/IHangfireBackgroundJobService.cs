namespace Task_Management_API.Interface
{
    public interface IHangfireBackgroundJobService
    {
        Task DailyRecurranceJob();
        Task WeeklyRecurranceJob();
        Task MonthlyRecurranceJob();

    }
}
