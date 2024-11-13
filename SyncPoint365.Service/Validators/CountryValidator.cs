using FluentValidation;
using SyncPoint365.Core.DTOs.Countries;

namespace SyncPoint365.Service.Validators
{
    public class CountryAddValidator : AbstractValidator<CountryAddDTO>
    {
        public CountryAddValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is required");
        }
    }

    public class CountryUpdateValidator : AbstractValidator<CountryUpdateDTO>
    {
        public CountryUpdateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is required");
        }
    }
}
