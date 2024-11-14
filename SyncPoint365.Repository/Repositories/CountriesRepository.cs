using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

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
    }
}
