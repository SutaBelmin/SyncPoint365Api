using AutoMapper;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class AbsenceRequestProfile : Profile
    {
        public AbsenceRequestProfile()
        {
            CreateMap<AbsenceRequestAddDTO, AbsenceRequest>().ReverseMap();
            CreateMap<AbsenceRequestUpdateDTO, AbsenceRequest>().ReverseMap();
            CreateMap<AbsenceRequestDTO, AbsenceRequest>().ReverseMap();

        }
    }
}
