using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICountriesService : IBaseService<CountryDTO, CountryAddDTO, CountryUpdateDTO>
    {
        Task<IEnumerable<CountryDTO>> GetCountriesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<CountryDTO>> GetPagedCountriesAsync(string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default);
    }
}
