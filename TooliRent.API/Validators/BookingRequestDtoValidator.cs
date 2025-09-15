using FluentValidation;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Validators
{
    public class BookingRequestDtoValidator : AbstractValidator<BookingRequestDto>
    {
        public BookingRequestDtoValidator()
        {
            RuleFor(x => x.ToolName).NotNull().NotEmpty().WithMessage("ToolName is required.");
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("StartDate must be before EndDate.");
            RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("EndDate must be in the future.");  
            RuleFor(x => x.EndDate).NotEqual(x => x.StartDate).WithMessage("EndDate must be different from StartDate.");   
            RuleFor(x => x.StartDate).LessThan(DateTime.Now.AddDays(7)).WithMessage("StartDate must be within the next 7 days.");
            RuleFor(x => x.EndDate).LessThan(DateTime.Now.AddDays(14)).WithMessage("EndDate must be within the next 14 days.");
        }
    }
}
