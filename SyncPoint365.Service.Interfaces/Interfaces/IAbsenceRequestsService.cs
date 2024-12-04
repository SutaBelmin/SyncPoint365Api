using SyncPoint365.Core.DTOs.AbsenceRequests;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestsService : IBaseService<AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>
    {
        Task<IEnumerable<AbsenceRequestDTO>> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<AbsenceRequestDTO>> GetAbsenceRequestsPagedListAsync(string? nameQuery, string? typeQuery, DateTime dateFrom, DateTime? dateTo, int page, int pageSize, CancellationToken cancellationToken = default);
    }
}
