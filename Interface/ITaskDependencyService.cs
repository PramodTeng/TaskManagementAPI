using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.Data.Entity;
using Task_Management_API.Model.RequestModel;
using Task_Management_API.Model.ResponseModel;

namespace Task_Management_API.Interface
{
    public interface ITaskDependencyService
    {
        Task<ActionResult> AddDependencies(int id, IEnumerable<int> dependencyIds);
        Task<ActionResult> RemoveDependencies(int id, IEnumerable<int> dependencyIds);
        Task<ActionResult<IEnumerable<TaskItem>>> GetTasks(DataRequestModel dataRequestModel);
        Task<ActionResult<IEnumerable<TaskResponseModel>>> SearchTasks(string query);
    }        
 }
