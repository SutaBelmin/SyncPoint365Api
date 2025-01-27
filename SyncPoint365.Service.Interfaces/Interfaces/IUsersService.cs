using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IUsersService : IBaseService<UserDTO, UserAddDTO, UserUpdateDTO>
    {
        Task<IEnumerable<UserDTO>> GetUsersListAsync(CancellationToken cancellationToken = default);
        Task<bool> EmailExists(string email);

        Task<IPagedList<UserDTO>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, string? loggedUserRole = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default);

        Task<bool> ChangeUserStatusAsync(int id, int loggedUserId, CancellationToken cancellationToken = default);
        Task<bool> ChangePasswordAsync(int id, string password, CancellationToken cancellationToken);

    }
}
