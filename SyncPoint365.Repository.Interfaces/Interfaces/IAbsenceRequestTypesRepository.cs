using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestTypesRepository : IBaseRepository<AbsenceRequestType>
    {
        Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<AbsenceRequestType>> GetPagedAbsenceRequestTypesListAsync(bool isActive, string? query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
