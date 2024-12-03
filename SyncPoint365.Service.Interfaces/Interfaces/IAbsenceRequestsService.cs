using SyncPoint365.Core.DTOs.AbsenceRequests;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestsService : IBaseService<AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>
    {
        Task<IEnumerable<AbsenceRequestDTO>> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default);
    }
}
