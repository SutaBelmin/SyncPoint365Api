using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.CompanyNews;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("company-news")]
    [ApiController]
    public class CompanyNewsController : BaseController<CompanyNewsDTO, CompanyNewsAddDTO, CompanyNewsUpdateDTO>
    {
        protected ICompanyNewsService _companyNewsService;
        public CompanyNewsController(ICompanyNewsService service) : base(service)
        {
            _companyNewsService = service;
        }

        [HttpGet]
        [Route("paged", Name = "SyncPoint365-GetCompanyNewsPagedListAsync")]
        public async Task<IActionResult> GetCompanyNewsPagedListAsync(string? query = null, bool? visible = null, DateTime? dateFrom = null, DateTime? dateTo = null, string? orderBy = null,
            int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {

            var items = await _companyNewsService.GetCompanyNewsPagedListAsync(query, visible, dateFrom, dateTo, orderBy, page, pageSize, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }

        [HttpPut]
        [Route("Change-visibility", Name = "SyncPoint365-ChangeVisibility")]
        public async Task<IActionResult> UpdateVisibility([FromQuery] int id, [FromBody] bool isVisible, CancellationToken cancellationToken)
        {
            var result = await _companyNewsService.UpdateVisibilityAsync(id, isVisible, cancellationToken);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

    }
}
