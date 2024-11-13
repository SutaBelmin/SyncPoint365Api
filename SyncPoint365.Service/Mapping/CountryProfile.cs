using AutoMapper;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.Entities;


namespace SyncPoint365.Service.Mapping
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryAddDTO, Country>().ReverseMap();
            CreateMap<CountryUpdateDTO, Country>().ReverseMap();
            CreateMap<CountryDTO, Country>().ReverseMap();
        }
    }
}
