using FluentValidation;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Validators
{
    public class UpdateCategoryRequestDtoValidator : AbstractValidator<UpdateCategoryRequestDto>
    {
        public UpdateCategoryRequestDtoValidator()
        {           
            RuleFor(x => x.NewCategoryName)
                .NotEmpty().WithMessage("New category name is required.")
                .MaximumLength(100).WithMessage("New category name must not exceed 100 characters.");
        }
    }
}
