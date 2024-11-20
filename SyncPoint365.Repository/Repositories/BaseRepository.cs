using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity>
    where TEntity : BaseEntity
    {
        protected readonly DatabaseContext DatabaseContext;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(DatabaseContext databaseContext)
        {
            DatabaseContext = databaseContext;
            DbSet = DatabaseContext.Set<TEntity>();
        }

        public virtual Task<IPagedList<TEntity>> GetAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            if (page == -1)
                return DbSet.ToPagedListAsync(1, int.MaxValue);
            else
                return DbSet.ToPagedListAsync(page, pageSize);
        }

        public virtual Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public virtual async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            await DbSet.AddAsync(entity, cancellationToken);
        }

        public virtual async void Update(TEntity entity, CancellationToken cancellationToken = default)
        {
            DatabaseContext.ChangeTracker.Clear();
            DbSet.Update(entity);
        }

        public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            await DbSet.Where(u => u.Id == id).ExecuteDeleteAsync(cancellationToken);
        }

        public virtual async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var entries = DatabaseContext.ChangeTracker.Entries<BaseEntity>();

                foreach (var entry in entries)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Entity.DateCreated = DateTime.UtcNow;
                        entry.Entity.DateUpdated = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property(e => e.DateCreated).IsModified = false;
                        entry.Entity.DateUpdated = DateTime.UtcNow;
                    }
                }

                await DatabaseContext.SaveChangesAsync(cancellationToken);

            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
