using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Enums;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestsService : IBaseService<AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>
    {
        Task<IPagedList<AbsenceRequestDTO>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, int? absenceRequestStatusId, DateTime? dateFrom, DateTime? dateTo,
            int? year, string? orderBy, int page, int pageSize, CancellationToken cancellationToken = default);

        Task<AbsenceRequestStatus> ChangeAbsenceRequestStatusAsync(int id, AbsenceRequestStatus newStatus, string? postComment, CancellationToken cancellationToken = default);
    }
}
