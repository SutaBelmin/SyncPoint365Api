using SyncPoint365.Core.Entities;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestTypesRepository : IBaseRepository<AbsenceRequestType>
    {
        Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(bool? isActive, CancellationToken cancellationToken = default);
        Task<IPagedList<AbsenceRequestType>> GetAbsenceRequestTypesPagedListAsync(bool? isActive, string? query, string? orderBy, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
