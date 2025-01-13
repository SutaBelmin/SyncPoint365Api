using FluentValidation;
using SyncPoint365.Core.DTOs.CompanyDocuments;

namespace SyncPoint365.Service.Validators
{
    public class CompanyDocumentAddValidator : AbstractValidator<CompanyDocumentAddDTO>
    {
        public CompanyDocumentAddValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is not valid");
        }
    }

    public class CompanyDocumentUpdateValidator : AbstractValidator<CompanyDocumentUpdateDTO>
    {
        public CompanyDocumentUpdateValidator()
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("Name is not valid");
        }
    }
}
