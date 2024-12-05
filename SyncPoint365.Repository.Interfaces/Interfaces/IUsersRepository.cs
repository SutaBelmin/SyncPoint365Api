using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetUsersListAsync(CancellationToken cancellationToken = default);

        Task UpdateUserStatusAsync(User user, CancellationToken cancellationToken = default);
        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

    }
}
