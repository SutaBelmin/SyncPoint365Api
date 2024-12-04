using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestsRepository : IBaseRepository<AbsenceRequest>
    {
        Task<IEnumerable<AbsenceRequest>> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<AbsenceRequest>> GetAbsenceRequestsPagedListAsync(string? nameQuery, string? typeQuery, DateTime dateFrom, DateTime? dateTo, int page, int pageSize, CancellationToken cancellationToken);
    }

}
