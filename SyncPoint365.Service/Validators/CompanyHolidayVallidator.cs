using FluentValidation;
using SyncPoint365.Core.DTOs.CompanyHolidays;

namespace SyncPoint365.Service.Validators
{
    public class CompanyHolidayAddVallidator : AbstractValidator<CompanyHolidayAddDTO>
    {
        public CompanyHolidayAddVallidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is not valid");
            RuleFor(c => c.Date).NotNull().NotEmpty().WithMessage("Date is not valid");
            RuleFor(c => c.UserId).NotNull().NotEmpty().WithMessage("UserId is not valid");
        }
    }

    public class CompanyHolidayUpdateValidator : AbstractValidator<CompanyHolidayUpdateDTO>
    {
        public CompanyHolidayUpdateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is not valid");
            RuleFor(c => c.Date).NotNull().NotEmpty().WithMessage("Date is not valid");
            RuleFor(c => c.UserId).NotNull().NotEmpty().WithMessage("UserId is not valid");
        }
    }
}
