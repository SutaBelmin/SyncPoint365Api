using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshToken?> GetRefreshTokenByUserIdAsync(int userId);
    }
}
