using Microsoft.EntityFrameworkCore;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            InitializeDatabase();
        }
        public DbSet<AbsenceRequest> AbsenceRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<AbsenceRequestType> AbsenceRequestTypes { get; set; }
        public DbSet<City> Cities { get; set; }

        private void InitializeDatabase()
        {
            this.Database.EnsureCreated();

            DatabaseSeed.Seed(this);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
