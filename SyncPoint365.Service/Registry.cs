using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.DTOs.CompanyHolidays;
using SyncPoint365.Core.DTOs.CompanyNews;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.DTOs.RefreshTokens;
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
            services.AddScoped<IValidator<AbsenceRequestAddDTO>, AbsenceRequestAddValidator>();
            services.AddScoped<IValidator<AbsenceRequestUpdateDTO>, AbsenceRequestUpdateValidator>();
            services.AddScoped<IValidator<UserAddDTO>, UserAddValidator>();
            services.AddScoped<IValidator<UserUpdateDTO>, UserUpdateValidator>();
            services.AddScoped<IValidator<AbsenceRequestTypeAddDTO>, AbsenceRequestTypeAddValidator>();
            services.AddScoped<IValidator<AbsenceRequestTypeUpdateDTO>, AbsenceRequestTypeUpdateValidator>();
            services.AddScoped<IValidator<CountryAddDTO>, CountryAddValidator>();
            services.AddScoped<IValidator<CountryUpdateDTO>, CountryUpdateValidator>();
            services.AddScoped<IValidator<CityAddDTO>, CityAddValidator>();
            services.AddScoped<IValidator<CityUpdateDTO>, CityUpdateValidator>();
            services.AddScoped<IValidator<CompanyDocumentAddDTO>, CompanyDocumentAddValidator>();
            services.AddScoped<IValidator<CompanyDocumentUpdateDTO>, CompanyDocumentUpdateValidator>();
            services.AddScoped<IValidator<RefreshTokenAddDTO>, RefreshTokenAddValidator>();
            services.AddScoped<IValidator<RefreshTokenUpdateDTO>, RefreshTokenUpdateValidator>();
            services.AddScoped<IValidator<CompanyNewsAddDTO>, CompanyNewsAddValidator>();
            services.AddScoped<IValidator<CompanyNewsUpdateDTO>, CompanyNewsUpdateValidator>();
            services.AddScoped<IValidator<CompanyHolidayAddDTO>, CompanyHolidayAddVallidator>();
            services.AddScoped<IValidator<CompanyHolidayUpdateDTO>, CompanyHolidayUpdateValidator>();
        }

        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAbsenceRequestsService, AbsenceRequestsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAbsenceRequestTypesService, AbsenceRequestTypesService>();
            services.AddScoped<ICountriesService, CountriesService>();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICompanyNewsService, CompanyNewsService>();
            services.AddScoped<IEnumsService, EnumsService>();
            services.AddScoped<ICompanyDocumentsService, CompanyDocumentsService>();
            services.AddScoped<IRefreshTokensService, RefreshTokensService>();
            services.AddScoped<ICompanyHolidaysService, CompanyHolidaysService>();

        }

        public static void AddMapping(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
