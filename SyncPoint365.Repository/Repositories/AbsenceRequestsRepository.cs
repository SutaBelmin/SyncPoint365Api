using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

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

        public Task<IPagedList<AbsenceRequest>> GetAbsenceRequestsPagedListAsync(string? query, DateTime dateFrom, DateTime dateTo, int page, int pageSize, CancellationToken cancellationToken)
        {
            var x = DbSet.Include(x => x.AbsenceRequestType).Include(c => c.User);
            return x.Where(a => ((string.IsNullOrWhiteSpace(query) || (a.User.FirstName + " " + a.User.LastName).ToLower().Contains(query.ToLower()))
            && (a.DateTo <= dateTo && a.DateFrom >= dateFrom)))
                .ToPagedListAsync(page, pageSize);
        }
    }
}
