using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestTypesService : IBaseService<AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>
    {
        Task<IEnumerable<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default);
        Task<IPagedList<AbsenceRequestTypeDTO>> GetPagedAbsenceRequestTypesListAsync(bool isActive, string? query, int page, int pageSize, CancellationToken cancellationToken);
    }
}
