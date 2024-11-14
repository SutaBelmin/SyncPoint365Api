using SyncPoint365.Core.DTOs.AbsenceRequestTypes;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IAbsenceRequestTypesService : IBaseService<AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>
    {
        Task<IEnumerable<AbsenceRequestTypeDTO>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default);
    }
}
