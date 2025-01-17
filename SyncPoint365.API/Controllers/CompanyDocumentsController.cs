using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyDocumentsController : BaseController<CompanyDocumentDTO, CompanyDocumentAddDTO, CompanyDocumentUpdateDTO>
    {
        private readonly ICompanyDocumentsService _companyDocumentsService;
        public CompanyDocumentsController(ICompanyDocumentsService companyDocumentsService) : base(companyDocumentsService)
        {
            _companyDocumentsService = companyDocumentsService;
        }


        [HttpGet]
        [Route("Paged", Name = "SyncPoint365-GetDocumentsPaged")]
        public async Task<IActionResult> GetPagedDocumentsAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var data = await _companyDocumentsService.GetPagedDocumentsAsync(query, page, pageSize, cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(GetPagedResult(data));
        }

    }
}
