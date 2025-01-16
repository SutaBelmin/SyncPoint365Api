using AutoMapper;
using FluentValidation;
using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Service.Common.Interfaces;
using System.Security.Cryptography;

namespace SyncPoint365.Service.Services
{
    public class RefreshTokensService : BaseService<RefreshToken, RefreshTokenDTO, RefreshTokenAddDTO, RefreshTokenUpdateDTO>, IRefreshTokensService
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;

        public RefreshTokensService(IRefreshTokensRepository refreshTokensRepository, IMapper mapper, IValidator<RefreshTokenAddDTO> addValidator, IValidator<RefreshTokenUpdateDTO> updateValidator)
            : base(refreshTokensRepository, mapper, addValidator, updateValidator)
        {
            _refreshTokensRepository = refreshTokensRepository;
        }

        public async Task<RefreshTokenDTO?> GetByTokenAsync(string token)
        {
            var refreshToken = await _refreshTokensRepository.GetByTokenAsync(token);
            if (refreshToken == null || refreshToken.ExpirationDate < DateTime.UtcNow)
            {
                return null;
            }
            return Mapper.Map<RefreshTokenDTO>(refreshToken);
        }

        public async Task<RefreshTokenDTO> GenerateAndSaveRefreshTokenAsync(int userId)
        {
            var token = GenerateRefreshToken();
            var expirationDate = DateTime.UtcNow.AddDays(7);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = expirationDate,
                DateUpdated = DateTime.UtcNow,
            };

            await _refreshTokensRepository.AddAsync(refreshToken);
            await _refreshTokensRepository.SaveChangesAsync();

            return Mapper.Map<RefreshTokenDTO>(refreshToken);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }
            return Convert.ToBase64String(randomNumber);
        }

        public async Task RemoveExpiredTokensAsync()
        {
            var allTokens = await _refreshTokensRepository.GetAsync();

            var expiredTokens = allTokens.Where(x => x.ExpirationDate < DateTime.UtcNow).ToList();

            if (expiredTokens.Any())
            {
                foreach (var token in expiredTokens)
                {
                    await _refreshTokensRepository.DeleteAsync(token.Id);
                }

                await _refreshTokensRepository.SaveChangesAsync();
            }
        }
    }
}
