using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IAbsenceRequestTypesRepository : IBaseRepository<AbsenceRequestType>
    {
        Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default);

    }
}
