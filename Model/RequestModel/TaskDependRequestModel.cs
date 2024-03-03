using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task_Management_API.Model.RequestModel
{
    public class TaskDependRequestModel
    {
        public int DependencyId { get; set; }
        public int TaskItemId { get; set; }
        public int DependentTaskItemId { get; set; }
    }
}