using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class CompanyDocumentsRepository : BaseRepository<CompanyDocument>, ICompanyDocumentsRepository
    {
        public CompanyDocumentsRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public Task<IPagedList<CompanyDocument>> GetPagedCompanyDocumentsAsync(DateTime? dateFrom, DateTime? dateTo, string? query = null, bool? isVisible = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            return DbSet.Include(x => x.User).Where(x =>
                                                       (string.IsNullOrWhiteSpace(query) ||
                                                       x.Name.ToLower().Contains(query.ToLower())) &&
                                                       (!dateFrom.HasValue || (x.DateCreated >= dateFrom && x.DateCreated <= dateTo)) &&
                                                       (!dateTo.HasValue || (x.DateCreated <= dateTo && x.DateCreated >= dateFrom)) &&
                                                       (!isVisible.HasValue || x.IsVisible == isVisible.Value))
                                                       .ToPagedListAsync(page == -1 ? 1 : page, page == -1 ? int.MaxValue : pageSize);
        }

    }
}
