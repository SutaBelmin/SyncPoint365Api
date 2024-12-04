using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class AbsenceRequestsRepository : BaseRepository<AbsenceRequest>, IAbsenceRequestsRepository
    {
        public AbsenceRequestsRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<AbsenceRequest>> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.Include(c => c.AbsenceRequestType)
                .Include(c => c.User)
                .ToListAsync();
        }
    }
}
