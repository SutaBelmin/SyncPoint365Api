using FluentValidation;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Service.Validators
{
    public class CountriesAddValidator : AbstractValidator<CountriesAddDTO>
    {
        public CountriesAddValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("First name is not valid");
        }
    }

    public class CountriesUpdateValidator : AbstractValidator<CountriesUpdateDTO>
    {
        public CountriesUpdateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("First name is not valid");
        }
    }
}
