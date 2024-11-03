using Microsoft.EntityFrameworkCore;
using SyncPoint365.API.Config;
using SyncPoint365.Repository;

namespace SyncPoint365.API
{
    public static class Registry
    {
        public static void AddConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(connectionStringsSection);
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            var connectionStrings = connectionStringsSection.Get<ConnectionStrings>();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionStrings!.Main));
        }
    }
}
