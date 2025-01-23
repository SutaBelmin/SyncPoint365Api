using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Policy = "AdminEmployeePolicy")]
        public async Task<IActionResult> GetPagedCompanyDocumentsAsync(DateTime? dateFrom, DateTime? dateTo, string? query = null, bool? isVisible = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var data = await _companyDocumentsService.GetPagedCompanyDocumentsAsync(dateFrom, dateTo, query, isVisible, page, pageSize, cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(GetPagedResult(data));
        }

        [HttpPatch]
        [Route("Update-Company-Document-Visibility", Name = "SyncPoint365-UpdateCompanyDocumentVisibility")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> UpdateCompanyDocumentVisibiltyAsync(int id, CancellationToken cancellationToken = default)
        {
            var result = await _companyDocumentsService.UpdateCompanyDocumentVisibiltyAsync(id, cancellationToken);

            if (result)
                return Ok(new { message = "Document visibility updated successfully." });

            return NotFound();
        }

        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public override Task<IActionResult> AddAsync(CompanyDocumentAddDTO dto, CancellationToken cancellationToken = default)
        {
            return base.AddAsync(dto, cancellationToken);
        }

        [HttpPut]
        [Authorize(Policy = "AdminPolicy")]
        public override Task<IActionResult> UpdateAsync(CompanyDocumentUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            return base.UpdateAsync(dto, cancellationToken);
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "AdminPolicy")]
        public override Task<IActionResult> DeleteAsync(int? id, CancellationToken cancellationToken = default)
        {
            return base.DeleteAsync(id, cancellationToken);
        }

    }
}
