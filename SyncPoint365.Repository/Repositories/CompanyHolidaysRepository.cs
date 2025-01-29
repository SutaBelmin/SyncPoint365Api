using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class CompanyHolidaysRepository : BaseRepository<CompanyHoliday>, ICompanyHolidaysRepository
    {
        public CompanyHolidaysRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }
    }
}
