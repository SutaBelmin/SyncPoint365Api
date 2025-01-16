using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.CompanyNews;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class CompanyNewsService : BaseService<CompanyNews, CompanyNewsDTO, CompanyNewsAddDTO, CompanyNewsUpdateDTO>, ICompanyNewsService
    {
        protected readonly ICompanyNewsRepository _repository;
        protected readonly IMapper _mapper;
        public CompanyNewsService(ICompanyNewsRepository repository,
            IMapper mapper,
            IValidator<CompanyNewsAddDTO> addValidator,
            IValidator<CompanyNewsUpdateDTO> updateValidator)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }
    }
}
