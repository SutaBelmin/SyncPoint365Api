using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;

namespace SyncPoint365.Service.Validators
{
    public class AbsenceRequestTypeAddValidator : AbstractValidator<AbsenceRequestTypeAddDTO>
    {
        public AbsenceRequestTypeAddValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("First name is not valid");
        }
    }
    public class AbsenceRequestTypeUpdateValidator : AbstractValidator<AbsenceRequestTypeUpdateDTO>
    {
        public AbsenceRequestTypeUpdateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("First name is not valid");
        }
    }
}
