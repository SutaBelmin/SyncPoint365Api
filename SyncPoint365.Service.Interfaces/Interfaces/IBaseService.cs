using SyncPoint365.Core.DTOs;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IBaseService<TDTO, TAddDTO, TUpdateDTO>
        where TDTO : BaseDTO
        where TAddDTO : BaseAddDTO
        where TUpdateDTO : BaseUpdateDTO
    {
        Task<IPagedList<TDTO>> GetAsync(string? query = null, int page = 1, CancellationToken cancellationToken = default);
        Task<TDTO?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TAddDTO dto, CancellationToken cancellationToken = default);
        Task UpdateAsync(TUpdateDTO dto, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
    }
}
