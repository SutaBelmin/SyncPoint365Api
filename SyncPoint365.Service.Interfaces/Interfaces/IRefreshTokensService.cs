using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IRefreshTokensService : IBaseService<RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>
    {
        Task<RefreshTokenDTO?> GetRefreshTokenByUserIdAsync(int userId);
        Task SaveRefreshTokenAsync(RefreshToken refreshToken);
    }
}
