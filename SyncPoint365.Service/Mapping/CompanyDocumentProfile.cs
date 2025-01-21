using AutoMapper;
using Microsoft.AspNetCore.Http;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class CompanyDocumentProfile : Profile
    {
        public CompanyDocumentProfile()
        {
            CreateMap<CompanyDocumentAddDTO, CompanyDocument>()
            .ForMember(dest => dest.File, opt => opt.MapFrom(src =>
                src.File != null ? GetFileBytes(src.File) : null));

            CreateMap<CompanyDocumentUpdateDTO, CompanyDocument>()
                .ForMember(dest => dest.File, opt =>
                {
                    opt.PreCondition(src => src.File != null);
                    opt.MapFrom(src => GetFileBytes(src.File));
                })
                .ForAllMembers(opts =>
                {
                    opts.Condition((src, dest, srcMember) => srcMember != null);
                });

            CreateMap<CompanyDocument, CompanyDocumentDTO>();

        }

        private static byte[] GetFileBytes(IFormFile file)
        {
            if (file == null)
                return Array.Empty<byte>();
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
