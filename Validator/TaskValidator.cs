using FluentValidation;
using Task_Management_API.Model.RequestModel;

namespace Task_Management_API.Validator
{
    public class TaskValidator : AbstractValidator<TaskRequestModel>
    {
        public TaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is Required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is Required");
            RuleFor(x => x.DueDate).NotEmpty().WithMessage("Due Date is Required").GreaterThan(DateTime.Now).WithMessage("Due Date Must be in Future");
        }
    }
}
