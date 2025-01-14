using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class CompanyDocumentsRepository : BaseRepository<CompanyDocument>, ICompanyDocumentsRepository
    {
        public CompanyDocumentsRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

    }
}
