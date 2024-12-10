using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<User?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
        }

        public async Task UpdateUserStatusAsync(User user, CancellationToken cancellationToken = default)
        {
            DbSet.Update(user);
            await SaveChangesAsync(cancellationToken);
        }

        public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default)
        {
            return await DbSet.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }

        public override Task<IPagedList<User>> GetAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {

            var users = DbSet.Include(x => x.City);

            if (page == -1)
                return users.ToPagedListAsync(1, int.MaxValue);
            else
                return users.ToPagedListAsync(page, pageSize);
        }

        public async Task<bool> EmailExists(string email)
        {
            return await DbSet.AnyAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public Task<IPagedList<User>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, int page = 1, int pageSize = 10, CancellationToken cancellationToken = default)
        {
            IQueryable<User> queryable = DbSet;

            if (!string.IsNullOrEmpty(query))
            {
                queryable = queryable.Where(x => x.FirstName.ToLower().Contains(query.ToLower()) || x.LastName.ToLower().Contains(query.ToLower()));
            }

            if (isActive.HasValue)
            {
                queryable = queryable.Where(x => x.isActive == isActive);
            }

            if (roleId.HasValue)
            {
                var parsedRole = (Role)roleId.Value;
                queryable = queryable.Where(x => x.Role == parsedRole);
            }

            if (page == -1)
                return queryable.ToPagedListAsync(1, int.MaxValue);
            else
                return queryable.ToPagedListAsync(page, pageSize);
        }
    }
}

