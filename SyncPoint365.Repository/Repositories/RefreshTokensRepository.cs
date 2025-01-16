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
    }
}
