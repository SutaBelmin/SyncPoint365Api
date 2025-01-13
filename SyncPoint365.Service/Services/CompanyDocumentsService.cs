using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class CompanyDocumentsService : BaseService<CompanyDocument, CompanyDocumentDTO, CompanyDocumentAddDTO, CompanyDocumentUpdateDTO>, ICompanyDocumentsService
    {
        public CompanyDocumentsService(ICompanyDocumentsRepository repository, IMapper mapper, IValidator<CompanyDocumentAddDTO> addValidator, IValidator<CompanyDocumentUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {

        }
    }
}
