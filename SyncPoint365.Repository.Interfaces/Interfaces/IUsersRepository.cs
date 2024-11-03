using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserId(int id, CancellationToken cancellationToken = default);
    }
}
