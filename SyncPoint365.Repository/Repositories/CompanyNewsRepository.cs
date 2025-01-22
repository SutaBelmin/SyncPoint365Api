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

        public async Task<IPagedList<CompanyNews>> GetCompanyNewsPagedListAsync(string? query, DateTime? dateFrom, DateTime? dateTo, string? orderBy, int page, int pageSize, CancellationToken cancellationToken)
        {

            var includes = DbSet.Include(c => c.User);

            return await includes.Where(c => (string.IsNullOrWhiteSpace(query) || c.Title.ToLower().Contains(query.ToLower()))
                && (!dateFrom.HasValue || (c.DateCreated >= dateFrom && c.DateCreated <= dateTo)))
                .Sort(string.IsNullOrWhiteSpace(orderBy) ? "DateCreated|desc" : orderBy)
                .ToPagedListAsync(page, pageSize);
        }

        public async Task<IPagedList<CompanyNews>> GetCompanyNewsVisibleListAsync(int page, int pageSize, CancellationToken cancellationToken)
        {
            var includes = DbSet.Include(c => c.User);

            return await includes
                .Where(c => c.IsVisible)
                .OrderByDescending(c => c.DateCreated)
                .ToPagedListAsync(page, pageSize);
        }


    }
}
