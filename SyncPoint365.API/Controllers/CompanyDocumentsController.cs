using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    public class CompanyDocumentsController : BaseController<CompanyDocumentDTO, CompanyDocumentAddDTO, CompanyDocumentUpdateDTO>
    {
        private readonly ICompanyDocumentsService _companyDocumentsService;
        public CompanyDocumentsController(ICompanyDocumentsService companyDocumentsService) : base(companyDocumentsService)
        {
            _companyDocumentsService = companyDocumentsService;
        }
    }
}
