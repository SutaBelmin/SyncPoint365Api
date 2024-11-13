using FluentValidation;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
