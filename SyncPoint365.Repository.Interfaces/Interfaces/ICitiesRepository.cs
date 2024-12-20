using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface ICitiesRepository : IBaseRepository<City>
    {
        Task<IEnumerable<City>> GetCitiesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<City>> GetPagedCitiesAsync(int? countryId = null, string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default);
    }
}
