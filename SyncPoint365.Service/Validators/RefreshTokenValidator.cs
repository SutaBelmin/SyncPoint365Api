using FluentValidation;
using SyncPoint365.Core.DTOs.RefreshTokens;

namespace SyncPoint365.Service.Validators
{
    public class RefreshTokenAddValidator : AbstractValidator<RefreshTokenAddDTO>
    {
        public RefreshTokenAddValidator()
        {
            RuleFor(r => r.Token).NotNull().NotEmpty().WithMessage("Token is required.");
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.ExpirationDate).GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        }

    }

    public class RefreshTokenUpdateValidator : AbstractValidator<RefreshTokenUpdateDTO>
    {
        public RefreshTokenUpdateValidator()
        {
            RuleFor(r => r.Token).NotNull().NotEmpty().WithMessage("Token is required.");
            RuleFor(x => x.UserId).NotNull().NotEmpty().WithMessage("User ID is required.");
            RuleFor(x => x.ExpirationDate).GreaterThan(DateTime.UtcNow).WithMessage("Expiration date must be in the future.");
        }

    }
}
