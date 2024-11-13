using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class AbsenceRequestTypesRepository : BaseRepository<AbsenceRequestType>, IAbsenceRequestTypesRepository
    {
        public AbsenceRequestTypesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<AbsenceRequestType?> GetByAbsenceRequestTypeIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
        }
    }
}
