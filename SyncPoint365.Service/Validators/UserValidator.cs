using FluentValidation;
using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Service.Validators
{
    public class UserAddValidator : AbstractValidator<UserAddDTO>
    {
        public UserAddValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().WithMessage("First name is not valid");
        }
    }

    public class UserUpdateValidator : AbstractValidator<UserUpdateDTO>
    {
        public UserUpdateValidator()
        {
            RuleFor(c => c.FirstName).NotNull().NotEmpty().WithMessage("First name is not valid");
        }
    }
}
