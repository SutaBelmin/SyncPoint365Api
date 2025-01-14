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
        private readonly ICompanyDocumentsRepository _repository;
        protected readonly IMapper _mapper;
        public CompanyDocumentsService(ICompanyDocumentsRepository repository, IMapper mapper, IValidator<CompanyDocumentAddDTO> addValidator, IValidator<CompanyDocumentUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task AddAsync(CompanyDocumentAddDTO dto, CancellationToken cancellationToken = default)
        {

            var companyDocument = _mapper.Map<CompanyDocument>(dto);

            using (var memoryStream = new MemoryStream())
            {
                await dto.File.CopyToAsync(memoryStream);
                companyDocument.File = memoryStream.ToArray();
            }

            companyDocument.ContentType = dto.ContentType;
            companyDocument.IsVisible = dto.IsVisible;
            companyDocument.UserId = dto.UserId;

            await _repository.AddAsync(companyDocument, cancellationToken);

        }

    }
}
