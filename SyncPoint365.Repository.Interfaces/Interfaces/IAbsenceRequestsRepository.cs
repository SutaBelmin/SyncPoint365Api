using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestsRepository : IBaseRepository<AbsenceRequest>
    {
        Task<IEnumerable<AbsenceRequest>> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default);
    }

}
