using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TooliRentClassLibrary.Models.DTO;

namespace TooliRent.API.Validators
{
    public class BookingRequestDtoValidator : AbstractValidator<BookingRequestDto>
    {
        public BookingRequestDtoValidator()
        {
            RuleFor(x => x.ToolId).GreaterThan(0).WithMessage("ToolId must be greater than 0.");
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate).WithMessage("StartDate must be before EndDate.");
            RuleFor(x => x.EndDate).GreaterThan(DateTime.Now).WithMessage("EndDate must be in the future.");           
        }
    }
}
