using AutoMapper;
using SyncPoint365.Core.DTOs.RefreshTokens;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class RefreshTokenProfile : Profile
    {
        public RefreshTokenProfile()
        {
            CreateMap<RefreshToken, RefreshTokenDTO>();
            CreateMap<RefreshTokenAddDTO, RefreshToken>();
            CreateMap<RefreshTokenUpdateDTO, RefreshToken>();
        }
    }
}
