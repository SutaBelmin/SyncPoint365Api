using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestTypesService : IBaseService<AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>
    {
        Task<IEnumerable<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesListAsync(bool? isActive, CancellationToken cancellationToken = default);
        Task<IPagedList<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesPagedListAsync(bool? isActive, string? query, int page, int pageSize, string? sortOrder, CancellationToken cancellationToken);
    }
}
