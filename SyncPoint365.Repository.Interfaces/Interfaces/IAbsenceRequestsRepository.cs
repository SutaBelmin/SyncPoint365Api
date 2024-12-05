using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestsRepository : IBaseRepository<AbsenceRequest>
    {
        Task<IPagedList<AbsenceRequest>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, DateTime? dateFrom, DateTime? dateTo, int page, int pageSize, CancellationToken cancellationToken);
    }

}
