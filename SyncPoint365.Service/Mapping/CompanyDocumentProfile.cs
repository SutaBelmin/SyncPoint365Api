using AutoMapper;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Entities;
using SyncPoint365.Service.Helpers;

namespace SyncPoint365.Service.Mapping
{
    public class CompanyDocumentProfile : Profile
    {
        public CompanyDocumentProfile()
        {
            CreateMap<CompanyDocumentAddDTO, CompanyDocument>()
            .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                src.File != null ? FileHelper.GetFileBytes(src.File) : null));

            CreateMap<CompanyDocumentUpdateDTO, CompanyDocument>()
                .ForMember(dest => dest.File, opt =>
                {
                    opt.PreCondition(src => src.File != null);
                    opt.MapFrom(src => FileHelper.GetFileBytes(src.File));
                })
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<CompanyDocument, CompanyDocumentDTO>();

        }
    }
}
