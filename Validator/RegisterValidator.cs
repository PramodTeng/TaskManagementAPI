using FluentValidation;
using Task_Management_API.Model;

namespace Task_Management_API.Validator
{
    public class RegisterValidator : AbstractValidator<RegisterModel>
    {

        public RegisterValidator()
        {

            RuleFor(x => x.Username).NotEmpty().WithMessage("UserName is Required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
        }
    }
}
