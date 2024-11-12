using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
<<<<<<< HEAD
using SyncPoint365.Core.DTOs.Countries;
=======
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
>>>>>>> 4822699 (Added AbsenceRequestType table construction and inherited CRUD methods.)
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

            services.AddScoped<IValidator<AbsenceRequestTypeAddDTO>, AbsenceRequestTypeAddValidator>();
            services.AddScoped<IValidator<AbsenceRequestTypeUpdateDTO>, AbsenceRequestTypeUpdateValidator>();
            
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<IAbsenceRequestTypeService, AbsenceRequestTypeService>();
        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }


    }
}
