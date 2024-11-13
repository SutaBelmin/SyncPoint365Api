using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Mapping;
using SyncPoint365.Service.Services;
using SyncPoint365.Service.Validators;

namespace SyncPoint365.Service
{
    public static class Registry
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<UserAddDTO>, UserAddValidator>();
            services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateValidator>();

            services.AddScoped<IValidator<CountryAddDTO>, CountryAddValidator>();
            services.AddScoped<IValidator<CountryUpdateDTO>, CountryUpdateValidator>();
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ICountriesService, CountriesService>();
        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
