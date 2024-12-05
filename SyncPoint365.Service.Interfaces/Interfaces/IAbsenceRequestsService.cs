using SyncPoint365.Core.DTOs.AbsenceRequests;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestsService : IBaseService<AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>
    {
        Task<IPagedList<AbsenceRequestDTO>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, DateTime? dateFrom, DateTime? dateTo, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
