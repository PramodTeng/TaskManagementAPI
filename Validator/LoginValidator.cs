using FluentValidation;
using Task_Management_API.Model;

namespace Task_Management_API.Validator
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is Required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is Required");
            
        }
    }
}
