using FluentValidation;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Validators
{
    public class AddToolRequestDtoValidator : AbstractValidator<AddToolRequestDto>
    {
        public AddToolRequestDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tool name is required.")
                .MaximumLength(100).WithMessage("Tool name cannot exceed 100 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be a positive integer.");
            RuleFor(x => x.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be a positive integer.");
            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid tool status.");
        }
    }
}
