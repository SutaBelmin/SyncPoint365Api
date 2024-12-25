using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Service.Services
{
    public class CitiesService : BaseService<City, CityDTO, CityAddDTO, CityUpdateDTO>, ICitiesService
    {
        private readonly ICitiesRepository _repository;
        protected readonly IMapper _mapper;
        public CitiesService(ICitiesRepository repository, IMapper mapper, IValidator<CityAddDTO> addValidator, IValidator<CityUpdateDTO> updateValidator) : base(repository, mapper, addValidator, updateValidator)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CityDTO>> GetCitiesListAsync(CancellationToken cancellationToken = default)
        {
            var cities = await _repository.GetCitiesListAsync();

            return _mapper.Map<IEnumerable<CityDTO>>(cities);
        }

        public async Task<IPagedList<CityDTO>> GetPagedCitiesAsync(int? countryId = null, string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var pagedList = await _repository.GetPagedCitiesAsync(countryId, query, orderBy, page, pageSize, cancellationToken);

            var dtos = Mapper.Map<List<CityDTO>>(pagedList);

            return new PagedList<CityDTO>(pagedList, dtos);

        }
    }
}
