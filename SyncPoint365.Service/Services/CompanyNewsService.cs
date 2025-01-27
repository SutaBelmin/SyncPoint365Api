using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using SyncPoint365.Core.DTOs.CompanyNews;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using System.Security.Claims;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public class CompanyNewsService : BaseService<CompanyNews, CompanyNewsDTO, CompanyNewsAddDTO, CompanyNewsUpdateDTO>, ICompanyNewsService
    {
        protected readonly ICompanyNewsRepository _repository;
        protected readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CompanyNewsService(ICompanyNewsRepository repository,
            IMapper mapper,
            IValidator<CompanyNewsAddDTO> addValidator,
            IValidator<CompanyNewsUpdateDTO> updateValidator,
            IHttpContextAccessor httpContextAccessor)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IPagedList<CompanyNewsDTO>> GetCompanyNewsPagedListAsync(string? query, bool? isVisible, DateTime? dateFrom, DateTime? dateTo, string? orderBy, int page, int pageSize, CancellationToken cancellationToken)
        {
            var paged = await _repository.GetCompanyNewsPagedListAsync(query, isVisible, dateFrom, dateTo, orderBy, page, pageSize, cancellationToken: cancellationToken);

            var dtos = Mapper.Map<List<CompanyNewsDTO>>(paged);

            return new PagedList<CompanyNewsDTO>(paged, dtos);
        }

        public async Task<bool> UpdateVisibilityAsync(int id, CancellationToken cancellationToken)
        {
            var companyNews = await _repository.GetByIdAsync(id);
            if (companyNews == null)
            {
                return false;
            }

            companyNews.IsVisible = !companyNews.IsVisible;
            await _repository.SaveChangesAsync(cancellationToken);
            return true;
        }

        public override async Task AddAsync(CompanyNewsAddDTO dto, CancellationToken cancellationToken = default)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out var userId))
            {
                dto.UserId = userId;
            }

            await base.AddAsync(dto, cancellationToken);
        }


    }
}
