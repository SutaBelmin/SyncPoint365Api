using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using X.PagedList;


namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IBaseRepository<TEntity>
       where TEntity : BaseEntity
    {
        Task<IPagedList<TEntity>> GetAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default);

        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
