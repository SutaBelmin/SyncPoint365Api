using SyncPoint365.Core.DTOs;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IBaseService<TDTO>
       where TDTO : BaseDTO
    {
        Task<TDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    }
}
