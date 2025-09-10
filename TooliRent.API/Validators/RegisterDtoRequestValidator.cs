using FluentValidation;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Validators
{
    public class RegisterDtoRequestValidator : AbstractValidator<RegisterDtoRequest>
    {
        public RegisterDtoRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required.");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid Email is required.");
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required.");
            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Passwords do not match.");
        }
    }
}
