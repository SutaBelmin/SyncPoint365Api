using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Repository.Common.Interfaces;

namespace SyncPoint365.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DatabaseContext DatabaseContext;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
            DbSet = DatabaseContext.Set<TEntity>();
        }

        public virtual Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await DatabaseContext.SaveChangesAsync(cancellationToken);

            }
            catch (Exception w)
            {

                throw;
            }
        }
    }
}
