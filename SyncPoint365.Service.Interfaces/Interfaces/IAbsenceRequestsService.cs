using SyncPoint365.Core.DTOs.AbsenceRequests;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestsService : IBaseService<AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>
    {
        Task<IPagedList<AbsenceRequestDTO>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, int? absenceRequestStatusId, DateTime? dateFrom, DateTime? dateTo,
            string? orderBy, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
