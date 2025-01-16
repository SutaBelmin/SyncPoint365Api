using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class CompanyNewsRepository : BaseRepository<CompanyNews>, ICompanyNewsRepository
    {
        public CompanyNewsRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
