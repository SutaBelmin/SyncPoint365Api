using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using SyncPoint365.Core.DTOs.CompanyHolidays;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.Service.Services
{
    public class CompanyHolidaysService : BaseService<CompanyHoliday, CompanyHolidayDTO, CompanyHolidayAddDTO, CompanyHolidayUpdateDTO>, ICompanyHolidaysService
    {
        private readonly ICompanyHolidaysRepository _repository;
        protected readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CompanyHolidaysService(ICompanyHolidaysRepository repository, IMapper mapper, IValidator<CompanyHolidayAddDTO> addValidator, IValidator<CompanyHolidayUpdateDTO> updateValidator, IConfiguration configuration)
            : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
            _configuration = configuration;
        }
    }
}
