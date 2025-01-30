using FluentValidation;
using SyncPoint365.Core.DTOs.CompanyNews;

namespace SyncPoint365.Service.Validators
{
    public class CompanyNewsAddValidator : AbstractValidator<CompanyNewsAddDTO>
    {
        public CompanyNewsAddValidator()
        {
            RuleFor(c => c.Title).NotNull().NotEmpty().WithMessage("Title is not valid");
            RuleFor(c => c.Text).NotNull().NotEmpty().WithMessage("Text is not valid");
        }
    }
    public class CompanyNewsUpdateValidator : AbstractValidator<CompanyNewsUpdateDTO>
    {
        public CompanyNewsUpdateValidator()
        {
            RuleFor(c => c.Title).NotNull().NotEmpty().WithMessage("Title is not valid");
            RuleFor(c => c.Text).NotNull().NotEmpty().WithMessage("Text is not valid");
        }
    }
}
