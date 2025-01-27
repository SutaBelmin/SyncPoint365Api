using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<AbsenceRequest> AbsenceRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AbsenceRequestType> AbsenceRequestTypes { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<CompanyDocument> CompanyDocuments { get; set; }

    }
}
