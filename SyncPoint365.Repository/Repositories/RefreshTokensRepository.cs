using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using System.Security.Cryptography;

namespace SyncPoint365.Repository.Repositories
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(DatabaseContext dbContext) : base(dbContext) { }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Token == token);
        }
        public async Task GenerateAndSaveRefreshTokenAsync(int userId)
        {
            var token = GenerateSecureToken();
            var expirationDate = DateTime.UtcNow.AddDays(7);

            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                ExpirationDate = expirationDate,
                DateCreated = DateTime.UtcNow
            };

            await DbSet.AddAsync(refreshToken);
            await SaveChangesAsync();
        }

        private string GenerateSecureToken()
        {
            using var rng = RandomNumberGenerator.Create();
            var randomBytes = new byte[64];
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

    }
}
