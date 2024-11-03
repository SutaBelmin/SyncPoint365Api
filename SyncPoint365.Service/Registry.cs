using Microsoft.Extensions.DependencyInjection;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Mapping;
using SyncPoint365.Service.Services;

namespace SyncPoint365.Service
{
    public static class Registry
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
