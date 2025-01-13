using Microsoft.Extensions.DependencyInjection;
using SyncPoint365.Repository.Common.Interfaces;
using SyncPoint365.Repository.Repositories;

namespace SyncPoint365.Repository
{
    public static class Registry
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAbsenceRequestsRepository, AbsenceRequestsRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICountriesRepository, CountriesRepository>();
            services.AddScoped<IAbsenceRequestTypesRepository, AbsenceRequestTypesRepository>();
            services.AddScoped<ICitiesRepository, CitiesRepository>();
            services.AddScoped<ICompanyDocumentsRepository, CompanyDocumentsRepository>();
        }
    }
}
