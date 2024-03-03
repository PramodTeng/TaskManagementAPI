using System.ComponentModel.DataAnnotations;
using Task_Management_API.Data.Entity;
using Task_Management_API.Data.Enum;

namespace Task_Management_API.Model.RequestModel
{
    public class TaskRequestModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Due date is required")]
        [DataType(DataType.DateTime)]
        [FutureDate(ErrorMessage = "Due date must be in the future")]
        public DateTime DueDate { get; set; }
        public DateTime StartDate { get; set; }
        public  StatusType Status { get; set; }
        public RecurrenceType Recurrence { get; set; }
        public TaskType TaskType { get; set; }
    }

    // Custom validation attribute for ensuring the due date is in the future
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return false;
        }
    }
}
