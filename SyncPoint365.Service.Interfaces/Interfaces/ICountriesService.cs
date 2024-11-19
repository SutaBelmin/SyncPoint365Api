using SyncPoint365.Core.DTOs.Countries;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICountriesService : IBaseService<CountryDTO, CountryAddDTO, CountryUpdateDTO>
    {
        Task<IEnumerable<CountryDTO>> GetCountriesListAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<CountryDTO>> SearchCountriesByNameAsync(string name, CancellationToken cancellationToken = default);
    }
}
