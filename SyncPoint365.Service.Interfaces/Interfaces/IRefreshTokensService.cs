using SyncPoint365.Core.DTOs.RefreshTokens;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IRefreshTokensService : IBaseService<RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>
    {
        Task<RefreshTokenDTO?> GetByTokenAsync(string token);
        Task<RefreshTokenDTO> GenerateAndSaveRefreshTokenAsync(int userId);
        Task RemoveExpiredTokensAsync();
    }
}
