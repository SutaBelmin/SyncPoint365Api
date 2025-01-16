using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using System.Linq.Dynamic.Core;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class AbsenceRequestsRepository : BaseRepository<AbsenceRequest>, IAbsenceRequestsRepository
    {
        public AbsenceRequestsRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public Task<IPagedList<AbsenceRequest>> GetAbsenceRequestsPagedListAsync(int? absenceRequestTypeId, int? userId, int? absenceRequestStatusId, DateTime? dateFrom, DateTime? dateTo,
            int? year, string? orderBy, int page, int pageSize, CancellationToken cancellationToken)
        {
            var includes = DbSet.Include(x => x.AbsenceRequestType).Include(c => c.User);

            return includes.Where(a => ((userId == null || a.UserId == userId.Value)
                && (absenceRequestTypeId == null || a.AbsenceRequestTypeId == absenceRequestTypeId.Value)
                && (!absenceRequestStatusId.HasValue || a.AbsenceRequestStatus == (AbsenceRequestStatus)absenceRequestStatusId.Value)
                && ((!dateFrom.HasValue || (a.DateFrom >= dateFrom && a.DateFrom <= dateTo))
                || (!dateTo.HasValue || (a.DateTo <= dateTo && a.DateTo >= dateFrom)))
                && (!year.HasValue || (a.DateTo.Year == year || a.DateFrom.Year == year))))
                .Sort(string.IsNullOrWhiteSpace(orderBy) ? "DateCreated|desc, AbsenceRequestStatus|asc, user.lastName|asc, user|firstName|asc" : orderBy)
                .ToPagedListAsync(page, pageSize);
        }
    }
}
