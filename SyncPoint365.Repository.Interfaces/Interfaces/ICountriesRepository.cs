using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface ICountriesRepository : IBaseRepository<Country>
    {
        Task<IEnumerable<Country>> GetCountriesListAsync(CancellationToken cancellationToken = default);
    }
}
