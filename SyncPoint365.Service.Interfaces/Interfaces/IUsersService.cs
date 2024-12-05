using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IUsersService : IBaseService<UserDTO, UserAddDTO, UserUpdateDTO>
    {
        Task<IEnumerable<UserDTO>> GetUsersListAsync(CancellationToken cancellationToken = default);
        Task ActivateDeactivateUserAsync(UserDeactivateDTO dto, CancellationToken cancellationToken = default);
    }
}
