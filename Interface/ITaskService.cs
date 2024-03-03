using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Data.Entity;
using Task_Management_API.Model.RequestModel;
using Task_Management_API.Model.ResponseModel;

namespace Task_Management_API.Interface
{
    public interface ITaskService
    {
        Task<TaskItem> CreateTaskAsync(TaskRequestModel model);
        Task<TaskItem> UpdateTaskAsync(TaskRequestModel model);
        Task<IEnumerable<TaskResponseModel>> GetTaskAsync();
        Task<TaskResponseModel> GetTasks(int id);
        Task DeleteAsync(int id);

    }
}
