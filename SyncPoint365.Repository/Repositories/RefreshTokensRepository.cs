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
    }
}
