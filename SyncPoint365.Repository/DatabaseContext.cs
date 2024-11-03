using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
