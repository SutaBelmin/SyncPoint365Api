using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IUsersService : IBaseService<UserDTO, UserAddDTO, UserUpdateDTO>
    {
        Task<IEnumerable<UserDTO>> GetUsersListAsync(CancellationToken cancellationToken = default);
        Task<bool> EmailExists(string email);

        Task<IPagedList<UserDTO>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, string? orderBy = null, CancellationToken cancellationToken = default);

        Task<bool> ToggleUserStatusAsync(int id, CancellationToken cancellationToken = default);
    }
}
