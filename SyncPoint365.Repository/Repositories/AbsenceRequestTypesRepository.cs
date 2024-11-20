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

        public Task<IPagedList<AbsenceRequestType>> GetPagedAbsenceRequestTypesListAsync(bool? isActive, string? query, int page, int pageSize, CancellationToken cancellationToken)
        {
            IQueryable<AbsenceRequestType> queryable = DbSet;

            if (isActive.HasValue)
            {
                queryable = queryable.Where(a => a.IsActive == isActive.Value);
            }

            else
            {
                queryable = queryable.Where(a => a.IsActive == true || a.IsActive == false);
            }

            if (!string.IsNullOrWhiteSpace(query))
            {
                queryable = queryable.Where(x => x.Name.ToLower().Contains(query.ToLower()));
            }

            if (page == -1)
            {
                return queryable.ToPagedListAsync(1, int.MaxValue);
            }
            else
            {
                return queryable.ToPagedListAsync(page, pageSize);
            }
        }
    }
}
