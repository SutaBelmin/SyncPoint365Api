using AutoMapper;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class AbsenceRequestProfile : Profile
    {
        public AbsenceRequestProfile()
        {
            CreateMap<AbsenceRequest, AbsenceRequestDTO>();
            CreateMap<AbsenceRequestAddDTO, AbsenceRequest>();
            CreateMap<AbsenceRequestUpdateDTO, AbsenceRequest>();
        }
    }
}
