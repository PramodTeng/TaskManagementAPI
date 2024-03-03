using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Management_API.Data.Entity;

namespace Task_Management_API.Model.ResponseModel
{
    public class TaskDependResponseModel
    {
        public TaskItem TaskItem { get; set; }
        public TaskItem DependentTaskItem { get; set; }
    }
}