using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class CitiesRepository : BaseRepository<City>, ICitiesRepository
    {
        public CitiesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }


        public async Task<IEnumerable<City>> GetCitiesListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet
                .Include(x => x.Country)
                .ToListAsync();
        }

        public Task<IPagedList<City>> GetPagedCitiesAsync(int? countryId = null, string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            return DbSet.Include(x => x.Country).Where(x =>
                                                           (string.IsNullOrWhiteSpace(query) || (x.Name.ToLower().Contains(query.ToLower()))
                                                           || (x.DisplayName.ToLower().Contains(query.ToLower())))
                                                           &&
                                                           (!countryId.HasValue || (x.CountryId == countryId.Value)))
                                                           .Sort(string.IsNullOrWhiteSpace(orderBy) ? "name|asc" : orderBy)
                                                           .ToPagedListAsync(page == -1 ? 1 : page, page == -1 ? int.MaxValue : pageSize);
        }
    }
}
