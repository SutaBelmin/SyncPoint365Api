using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Repository.Repositories
{
    public class AbsenceRequestTypeRepository : BaseRepository<AbsenceRequestType>, IAbsenceRequestTypeRepository
    {
        public AbsenceRequestTypeRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }
    }
}
