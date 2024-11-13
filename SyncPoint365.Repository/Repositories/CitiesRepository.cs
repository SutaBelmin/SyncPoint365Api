using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Include(x=> x.Country)
                .ToListAsync();
        }
    }
}
