using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class AbsenceRequestTypesRepository : BaseRepository<AbsenceRequestType>, IAbsenceRequestTypesRepository
    {
        public AbsenceRequestTypesRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
        }

        public Task<IPagedList<AbsenceRequestType>> GetPagedAbsenceRequestTypesListAsync(bool isActive, string? query, int page, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<AbsenceRequestType> quereiable = DbSet;

            if (!string.IsNullOrWhiteSpace(query))
            {
                quereiable = quereiable.Where(x => x.Name.ToLower().Contains(query.ToLower()));
            }

            quereiable = quereiable.Where(x => x.IsActive == isActive);

            if (page == -1)
            {
                return quereiable.ToPagedListAsync(1, int.MaxValue);
            }
            else
            {
                return quereiable.ToPagedListAsync(page, pageSize);
            }
        }
    }
}
