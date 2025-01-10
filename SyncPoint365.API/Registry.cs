using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SyncPoint365.API.Config;
using SyncPoint365.Repository;
using System.Text;

namespace SyncPoint365.API
{
    public static class Registry
    {
        public static void AddConfigs(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(connectionStringsSection);

            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            services.Configure<JWTSettings>(jwtSettingsSection);
        }

        public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionStringsSection = configuration.GetSection("ConnectionStrings");
            var connectionStrings = connectionStringsSection.Get<ConnectionStrings>();

            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionStrings!.Main));
        }

        public static void AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettingsSection = configuration.GetSection("JwtSettings");
            var jwtSettings = jwtSettingsSection.Get<JWTSettings>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = jwtSettings.Audience,
                    ValidateAudience = true,
                    RequireExpirationTime = false,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdminPolicy", policy => policy.RequireRole("SuperAdministrator"));
                options.AddPolicy("SuperAdminOrAdminPolicy", policy => policy.RequireRole("SuperAdministrator", "Administrator"));
                options.AddPolicy("SuperAdminOrAdminOrEmployeePolicy", policy => policy.RequireRole("SuperAdministrator", "Administrator", "Employee"));
            });
        }
    }
}
