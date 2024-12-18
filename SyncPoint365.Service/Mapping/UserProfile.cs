using AutoMapper;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
               .ForMember(d => d.FullName, o => o.MapFrom(e => e.FirstName + " " + e.LastName));

            CreateMap<UserAddDTO, User>();

            CreateMap<UserUpdateDTO, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore());

        }
    }
}
