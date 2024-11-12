using AutoMapper;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.Entities;


namespace SyncPoint365.Service.Mapping
{
    public class CountriesProfile : Profile
    {
        public CountriesProfile() 
        {
            CreateMap<CountriesAddDTO, Countries>().ReverseMap();
            CreateMap<CountriesUpdateDTO, Countries>().ReverseMap();
            CreateMap<CountriesDTO, Countries>().ReverseMap();
        }
    }
}
