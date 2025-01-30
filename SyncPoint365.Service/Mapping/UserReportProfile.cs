using AutoMapper;
using SyncPoint365.Core.DTOs.UserReports;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class UserReportProfile : Profile
    {
        public UserReportProfile()
        {
            CreateMap<User, UserDTO>();

            CreateMap<UserDTO, UserReportDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Title, opt => opt.Ignore());
        }
    }
}
