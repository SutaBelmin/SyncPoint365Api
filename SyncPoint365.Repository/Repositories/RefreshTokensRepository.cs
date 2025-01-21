using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
    {

        public RefreshTokensRepository(DatabaseContext dbContext) : base(dbContext) { }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Token == token);
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            var existingToken = await DbSet.FirstOrDefaultAsync(rt => rt.UserId == refreshToken.UserId);

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

        public async Task DeleteRefreshTokenAsync(string token)
        {
            var refreshToken = await DbSet.FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken != null)
            {
                DbSet.Remove(refreshToken);
                await SaveChangesAsync();
            }
        }

        public async Task<RefreshToken?> GetRefreshTokenByUserIdAsync(int userId)
        {
            return await DbSet.FirstOrDefaultAsync(rt => rt.UserId == userId && rt.ExpirationDate > DateTime.UtcNow);
        }


    }
}
