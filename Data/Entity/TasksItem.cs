using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Task_Management_API.Data.Enum;

namespace Task_Management_API.Data.Entity
{
    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public StatusType Status { get; set; }
        public ICollection<TaskDependency> Dependencies { get; set; }
        public RecurrenceType Recurrence { get; set; }
        public TaskType TaskType { get; set; }
        public bool IsCompleted { get; set; }
    }








}
