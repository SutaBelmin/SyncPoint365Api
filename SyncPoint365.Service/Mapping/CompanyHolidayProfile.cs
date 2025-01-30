using AutoMapper;
using SyncPoint365.Core.DTOs.CompanyHolidays;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class CompanyHolidayProfile : Profile
    {
        public CompanyHolidayProfile()
        {
            CreateMap<CompanyHolidayAddDTO, CompanyHoliday>();
            CreateMap<CompanyHolidayUpdateDTO, CompanyHoliday>();
            CreateMap<CompanyHoliday, CompanyHolidayDTO>();
        }
    }
}
