using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestsRepository : IBaseRepository<AbsenceRequest>
    {
        Task<IPagedList<AbsenceRequest>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, int? absenceRequestStatusId, DateTime? dateFrom, DateTime? dateTo,
            int? year, string? orderBy, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
