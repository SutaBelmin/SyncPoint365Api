using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class CountriesRepository : BaseRepository<Country>, ICountriesRepository
    {
        public CountriesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<Country>> GetCountriesListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
        }
        public override async Task<IPagedList<Country>> GetAsync(string? query, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            IQueryable<Country> queryable = DbSet;

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(c => c.Name.Contains(query) || c.DisplayName.Contains(query));
            }
            int totalSetCount = await queryable.CountAsync(cancellationToken);
            return await queryable.ToPagedListAsync(page, pageSize, totalSetCount, cancellationToken);
        }
        public async Task<IPagedList<Country>> GetPagedCountriesAsync(string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Where(x => string.IsNullOrEmpty(query) || x.Name.Contains(query) || x.DisplayName.Contains(query))
                .Sort(string.IsNullOrWhiteSpace(orderBy) ? "name|asc" : orderBy)
                .ToPagedListAsync(page == -1 ? 1 : page, page == -1 ? int.MaxValue : pageSize);
        }
    }
}
