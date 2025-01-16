using AutoMapper;
using SyncPoint365.Core.DTOs.CompanyNews;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class CompanyNewsProfile : Profile
    {
        public CompanyNewsProfile()
        {
            CreateMap<CompanyNews, CompanyNewsDTO>();
            CreateMap<CompanyNewsAddDTO, CompanyNews>();
            CreateMap<CompanyNewsUpdateDTO, CompanyNews>();
        }
    }
}
