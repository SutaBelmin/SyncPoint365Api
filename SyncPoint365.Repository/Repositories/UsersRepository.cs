using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<User?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
        }

        public async Task UpdateUserStatusAsync(User user, CancellationToken cancellationToken = default)
        {
            DbSet.Update(user);
            await SaveChangesAsync(cancellationToken);
        }
    }
}
