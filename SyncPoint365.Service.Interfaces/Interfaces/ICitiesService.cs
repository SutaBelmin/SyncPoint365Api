using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICitiesService : IBaseService<CityDTO, CityAddDTO, CityUpdateDTO>
    {
        Task<IEnumerable<CityDTO>> GetCitiesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<CityDTO>> GetPagedCitiesAsync(int? countryId = null, string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default);
    }
}
