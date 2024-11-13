using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestTypesRepository : IBaseRepository<AbsenceRequestType>
    {
        Task<AbsenceRequestType?> GetByAbsenceRequestTypeIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default);

    }
}
