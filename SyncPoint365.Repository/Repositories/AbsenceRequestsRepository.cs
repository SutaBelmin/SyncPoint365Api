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

        public Task<IPagedList<AbsenceRequest>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, DateTime? dateFrom, DateTime? dateTo, int page, int pageSize, CancellationToken cancellationToken)
        {
            var includes = DbSet.Include(x => x.AbsenceRequestType).Include(c => c.User);

            return includes.Where(a => ((userId == null || a.UserId == userId.Value)
            && (absenceRequestTypeId == null || a.AbsenceRequestTypeId == absenceRequestTypeId.Value)
            && ((!dateFrom.HasValue || a.DateFrom >= dateFrom)
            && (!dateTo.HasValue || a.DateTo <= dateTo))))
                .ToPagedListAsync(page, pageSize);
        }
    }
}
