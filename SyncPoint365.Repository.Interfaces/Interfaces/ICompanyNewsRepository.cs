using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface ICompanyNewsRepository : IBaseRepository<CompanyNews>
    {
        Task<IPagedList<CompanyNews>> GetCompanyNewsPagedListAsync(string? query, bool? isVisible, DateTime? dateFrom, DateTime? dateTo, string? orderBy, int page, int pageSize, CancellationToken cancellationToken);
    }
}
