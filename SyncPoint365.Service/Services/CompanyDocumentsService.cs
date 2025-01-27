using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public class CompanyDocumentsService : BaseService<CompanyDocument, CompanyDocumentDTO, CompanyDocumentAddDTO, CompanyDocumentUpdateDTO>, ICompanyDocumentsService
    {
        private readonly ICompanyDocumentsRepository _repository;
        protected readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CompanyDocumentsService(ICompanyDocumentsRepository repository, IMapper mapper, IValidator<CompanyDocumentAddDTO> addValidator, IValidator<CompanyDocumentUpdateDTO> updateValidator, IConfiguration configuration)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public override async Task AddAsync(CompanyDocumentAddDTO dto, CancellationToken cancellationToken = default)
        {
            await AddValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var extension = Path.GetExtension(dto.File.FileName).ToLower();
            var allowedExtension = _configuration.GetSection("FileSettings:AllowedDocumentExtensions").Get<List<string>>();

            if (allowedExtension == null || !allowedExtension.Contains(extension))
            {
                throw new Exception("Unsupported file format!");
            }

            var companyDocument = _mapper.Map<CompanyDocument>(dto);

            companyDocument.ContentType = dto.File.ContentType;

            await _repository.AddAsync(companyDocument, cancellationToken);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public override async Task UpdateAsync(CompanyDocumentUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            await UpdateValidator.ValidateAndThrowAsync(dto, cancellationToken);

            var companyDocument = await _repository.GetByIdAsync(dto.Id);
            if (companyDocument == null)
            {
                throw new KeyNotFoundException($"CompanyDocument with Id {dto.Id} not found.");
            }

            _mapper.Map(dto, companyDocument);
            if (dto.File != null)
            {
                companyDocument.ContentType = dto.File.ContentType;
            }
            _repository.Update(companyDocument);
            await _repository.SaveChangesAsync(cancellationToken);
        }

        public async Task<IPagedList<CompanyDocumentDTO>> GetPagedCompanyDocumentsAsync(DateTime? dateFrom, DateTime? dateTo, string? query = null, bool? isVisible = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var pagedList = await _repository.GetPagedCompanyDocumentsAsync(dateFrom, dateTo, query, isVisible, page, pageSize, cancellationToken);

            var dtos = Mapper.Map<List<CompanyDocumentDTO>>(pagedList);

            return new PagedList<CompanyDocumentDTO>(pagedList, dtos);
        }

        public async Task<bool> UpdateCompanyDocumentVisibiltyAsync(int id, CancellationToken cancellationToken = default)
        {
            var document = await _repository.GetByIdAsync(id);
            if (document == null)
                return false;

            document.IsVisible = !document.IsVisible;

            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
