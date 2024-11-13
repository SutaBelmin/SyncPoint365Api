using AutoMapper;
using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Service.Mapping
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<CityAddDTO, City>().ReverseMap();
            CreateMap<CityUpdateDTO, City>().ReverseMap(); 
            CreateMap<CityDTO, City>().ReverseMap();
        }
    }
}
