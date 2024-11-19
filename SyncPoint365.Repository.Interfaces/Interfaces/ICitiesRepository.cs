using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface ICitiesRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<City>> GetPagedCitiesAsync(int? countryId = null, string? query = null, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    }
}
