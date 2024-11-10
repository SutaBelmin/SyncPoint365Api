using X.PagedList;
using SyncPoint365.Core.Entities;


namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IBaseRepository<TEntity>
       where TEntity : BaseEntity
    {
        Task<IPagedList<TEntity>> GetAsync(string? query = null, int page = 1, CancellationToken cancellationToken = default);
        Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
        void Update(TEntity entity, CancellationToken cancellationToken = default);
        Task DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
