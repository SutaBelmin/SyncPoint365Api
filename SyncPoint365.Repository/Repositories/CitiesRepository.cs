using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
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

        public Task<IPagedList<City>> GetPagedCitiesAsync(int? countryId = null, string? query = null, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            IQueryable<City> queryable = DbSet.Include(x => x.Country);

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryable = queryable.Where(city => city.Name.ToLower().Contains(query.ToLower()));
            }

            if (countryId.HasValue)
            {
                queryable = queryable.Where(city => city.CountryId == countryId.Value);
            }


            if (page == -1)
                return queryable.ToPagedListAsync(1, int.MaxValue);
            else
                return queryable.ToPagedListAsync(page, pageSize);
        }
    }
}
