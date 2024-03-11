using System.ComponentModel.DataAnnotations;
using Task_Management_API.Data.Entity;
using Task_Management_API.Data.Enum;

namespace Task_Management_API.Model.RequestModel
{
    public class TaskRequestModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }     
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public  StatusType Status { get; set; }
        public RecurrenceType Recurrence { get; set; }
        public TaskType TaskType { get; set; }
    }
}
