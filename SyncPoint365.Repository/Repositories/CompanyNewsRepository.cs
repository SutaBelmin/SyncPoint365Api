using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class CompanyNewsRepository : BaseRepository<CompanyNews>, ICompanyNewsRepository
    {
        public CompanyNewsRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IPagedList<CompanyNews>> GetCompanyNewsPagedListAsync(DateTime? dateFrom, DateTime? dateTo, string? orderBy, int page, int pageSize, CancellationToken cancellationToken)
        {

            var query = DbSet.Include(c => c.User).Sort(string.IsNullOrWhiteSpace(orderBy) ? "DateCreated|desc" : orderBy);

            var totalSetCount = await query.CountAsync(cancellationToken);

            return await query.ToPagedListAsync(page, pageSize, totalSetCount, cancellationToken);
        }

    }
}
