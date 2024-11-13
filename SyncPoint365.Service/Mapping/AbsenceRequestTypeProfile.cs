using AutoMapper;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncPoint365.Service.Mapping
{
    public class AbsenceRequestTypeProfile : Profile
    {
        public AbsenceRequestTypeProfile()
        {
            CreateMap<AbsenceRequestTypeAddDTO, AbsenceRequestType>().ReverseMap();
            CreateMap<AbsenceRequestTypeUpdateDTO, AbsenceRequestType>().ReverseMap();
            CreateMap<AbsenceRequestTypeDTO, AbsenceRequestType>().ReverseMap();

        }
    }
}
