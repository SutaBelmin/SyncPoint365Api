using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
    {

        public RefreshTokensRepository(DatabaseContext dbContext) : base(dbContext) { }

        public async Task<RefreshToken?> GetRefreshTokenByUserIdAsync(int userId)
        {
            return await DbSet.FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            var existingToken = await DbSet.FirstOrDefaultAsync(t => t.UserId == refreshToken.UserId);

            if (existingToken != null)
            {
                existingToken.Token = refreshToken.Token;
                existingToken.ExpirationDate = refreshToken.ExpirationDate;
                DbSet.Update(existingToken);
            }
            else
            {
                await DbSet.AddAsync(refreshToken);
            }

            await SaveChangesAsync();
        }
    }
}
