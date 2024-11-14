using FluentValidation;
using SyncPoint365.Core.DTOs.Cities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Service.Validators
{
    public class CityAddValidator : AbstractValidator<CityAddDTO>
    {
        public CityAddValidator()
        {
            RuleFor(C=> C.Name).NotNull().NotEmpty().WithMessage("Name is required");
        }
    }

    public class CityUpdateValidator : AbstractValidator<CityUpdateDTO>
    {
        public CityUpdateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is required");
        }

    }
}
