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
        [Route("Paged", Name = "SyncPoint365-GetCompanyDocumentsPaged")]
        public async Task<IActionResult> GetPagedCompanyDocumentsAsync(DateTime? dateFrom, DateTime? dateTo, string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var data = await _companyDocumentsService.GetPagedCompanyDocumentsAsync(dateFrom, dateTo, query, page, pageSize, cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(GetPagedResult(data));
        }

        [HttpPatch]
        [Route("Update-Document-Visibility", Name = "SyncPoint365-UpdateDocumentVisibility")]
        public async Task<IActionResult> UpdateDocumentVisibiltyAsync(int documentId, bool isVisibile, CancellationToken cancellationToken = default)
        {
            var result = await _companyDocumentsService.UpdateDocumentVisibiltyAsync(documentId, isVisibile, cancellationToken);

            if (result)
                return Ok(new { message = "Document visibility updated successfully." });

            return NotFound();
        }

    }
}
