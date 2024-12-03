using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequests;

namespace SyncPoint365.Service.Validators
{
    public class AbsenceRequestAddValidator : AbstractValidator<AbsenceRequestAddDTO>
    {
        public AbsenceRequestAddValidator()
        {
            RuleFor(c => c.DateFrom).NotNull().NotEmpty().LessThanOrEqualTo(c => c.DateTo).WithMessage("Date entry from not valid");
            RuleFor(c => c.DateFrom).NotNull().NotEmpty().WithMessage("Date entry to not valid");
        }
    }

    public class AbsenceRequestUpdateValidator : AbstractValidator<AbsenceRequestUpdateDTO>
    {
        public AbsenceRequestUpdateValidator()
        {
            RuleFor(c => c.DateFrom).NotNull().NotEmpty().LessThanOrEqualTo(c => c.DateTo).WithMessage("Date entry from not valid");
            RuleFor(c => c.DateFrom).NotNull().NotEmpty().WithMessage("Date entry to not valid");
        }
    }
}
