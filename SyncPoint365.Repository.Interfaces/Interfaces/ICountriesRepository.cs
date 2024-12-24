using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface ICountriesRepository : IBaseRepository<Country>
    {
        Task<IEnumerable<Country>> GetCountriesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<Country>> GetPagedCountriesAsync(string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageNumber, CancellationToken cancellationToken = default);
    }
}
