using SyncPoint365.Core.DTOs.RefreshTokens;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IRefreshTokensService : IBaseService<RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>
    {
        Task<RefreshTokenDTO?> GetRefreshTokenByUserIdAsync(int userId);
        Task AddRefreshTokenAsync(int userId, string refreshToken, DateTime? tokenExpiration);
    }
}
