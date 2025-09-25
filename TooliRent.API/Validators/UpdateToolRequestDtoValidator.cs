using FluentValidation;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Validators
{
    public class UpdateToolRequestDtoValidator : AbstractValidator<UpdateToolRequestDto>
    {
        public UpdateToolRequestDtoValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Status must be a valid ToolStatus.");

        }
    }
}
