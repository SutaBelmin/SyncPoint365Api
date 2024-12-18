using SyncPoint365.Core.Entities;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Repository.Common.Interfaces
{
    public interface IUsersRepository : IBaseRepository<User>
    {
        Task<User?> GetByUserIdAsync(int id, CancellationToken cancellationToken = default);

        Task<IEnumerable<User>> GetUsersListAsync(CancellationToken cancellationToken = default);

        Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken = default);

        Task<bool> EmailExists(string email);

        Task<IPagedList<User>> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, string? orderBy = null, CancellationToken cancellationToken = default);

    }
}
