using AutoMapper;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Entities;

namespace SyncPoint365.Service.Mapping
{
    public class CompanyDocumentProfile : Profile
    {
        public CompanyDocumentProfile()
        {
            CreateMap<CompanyDocumentAddDTO, CompanyDocument>().ReverseMap();
            CreateMap<CompanyDocumentUpdateDTO, CompanyDocument>().ReverseMap();
            CreateMap<CompanyDocumentDTO, CompanyDocument>().ReverseMap();

        }
    }
}
