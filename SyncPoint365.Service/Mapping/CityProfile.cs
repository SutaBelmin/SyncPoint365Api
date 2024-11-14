using AutoMapper;
using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityAddDTO, City>();
            CreateMap<CityUpdateDTO, City>();
            CreateMap<City, CityDTO>();
        }
    }
}
