using SyncPoint365.Core.DTOs.Cities;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICitiesService : IBaseService<CityDTO, CityAddDTO, CityUpdateDTO>
    {
        Task<IEnumerable<CityDTO>> GetCitiesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<CityDTO>> GetPagedCitiesAsync(int? countryId = null, string? query = null, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}
