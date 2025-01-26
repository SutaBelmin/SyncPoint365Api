using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Enums;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Repository.Common.Interfaces;
using System.Linq.Dynamic.Core;
using X.PagedList;

namespace SyncPoint365.Repository.Repositories
{
    public class UsersRepository : BaseRepository<User>, IUsersRepository
    {

        public UsersRepository(DatabaseContext databaseContext) : base(databaseContext)
        {

        }

        public async override Task<User?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await DbSet.Include(u => u.City).FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<User>> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            return await DbSet.ToListAsync();
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

        public Task<IPagedList<User>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            return DbSet.Include(x => x.City).Where(user =>
                                                             (string.IsNullOrEmpty(query) || (user.FirstName + " " + user.LastName).ToLower().Contains(query.ToLower()))
                                                             &&
                                                             (!isActive.HasValue || user.IsActive == isActive) &&
                                                             (!roleId.HasValue || user.Role == (Role)roleId.Value))
                                                             .Sort(string.IsNullOrWhiteSpace(orderBy) ? "lastName|asc" : orderBy)
                                                             .ToPagedListAsync(page == -1 ? 1 : page, page == -1 ? int.MaxValue : pageSize);
        }

    }
}

