using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;


namespace SyncPoint365.Service.Services
{
    public class CountriesService : BaseService<Countries, CountriesDTO, CountriesAddDTO, CountriesUpdateDTO>, ICountriesService
    {
        private readonly ICountriesRepository _repository;
        protected readonly IMapper _mapper;
        public CountriesService(ICountriesRepository repository, IMapper mapper, IValidator<CountriesAddDTO> addValidator, IValidator<CountriesUpdateDTO> updateValidator) : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

    }
}
