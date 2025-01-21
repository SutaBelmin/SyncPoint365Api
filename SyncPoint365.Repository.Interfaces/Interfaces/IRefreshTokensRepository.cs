using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshToken>
    {
        Task<RefreshToken> GetByTokenAsync(string token);
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
        Task DeleteRefreshTokenAsync(string token);
        Task<RefreshToken?> GetRefreshTokenByUserIdAsync(int userId);

    }
}
