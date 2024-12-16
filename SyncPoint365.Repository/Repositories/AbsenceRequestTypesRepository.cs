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

        public async Task<IEnumerable<AbsenceRequestType>> GetAbsenceRequestTypesListAsync(bool? isActive, CancellationToken cancellationToken = default)
        {
            return await DbSet.Where(a => (!isActive.HasValue || a.IsActive == isActive.Value)).ToListAsync(cancellationToken);
        }

        public Task<IPagedList<AbsenceRequestType>> GetAbsenceRequestTypesPagedListAsync(bool? isActive, string? query, int page, int pageSize, CancellationToken cancellationToken)
        {
            return DbSet.Where(a => (isActive == null || a.IsActive == isActive) && (string.IsNullOrWhiteSpace(query) || a.Name.ToLower().Contains(query.ToLower())))
                .ToPagedListAsync(page, pageSize);

        }
    }
}