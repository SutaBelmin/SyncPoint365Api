using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;


namespace SyncPoint365.Service.Services
{
    public class CountriesService : BaseService<Country, CountryDTO, CountryAddDTO, CountryUpdateDTO>, ICountriesService
    {
        private readonly ICountriesRepository _repository;
        protected readonly IMapper _mapper;
        public CountriesService(ICountriesRepository repository, IMapper mapper, IValidator<CountryAddDTO> addValidator, IValidator<CountryUpdateDTO> updateValidator) : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CountryDTO>> GetCountriesListAsync(CancellationToken cancellationToken = default)
        {
            var countries = await _repository.GetCountriesListAsync();

            return _mapper.Map<IEnumerable<CountryDTO>>(countries);
        }

        public async Task<IEnumerable<CountryDTO>> SearchCountriesByNameAsync(string name, CancellationToken cancellationToken = default)
        {
            var countries = await _repository.SearchCountriesByNameAsync(name);
            return _mapper.Map<IEnumerable<CountryDTO>>(countries);
        }
    }
}
