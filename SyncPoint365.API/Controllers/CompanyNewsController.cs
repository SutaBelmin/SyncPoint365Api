using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.API.Helpers;
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
        [Authorize(Policy = "AdminEmployeePolicy")]
        public async Task<IActionResult> GetCompanyNewsPagedListAsync(string? query = null, bool? isVisible = null, DateTime? dateFrom = null, DateTime? dateTo = null, string? orderBy = null,
            int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {

            var items = await _companyNewsService.GetCompanyNewsPagedListAsync(query, isVisible, dateFrom, dateTo, orderBy, page, pageSize, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }

        [HttpPut]
        [Route("Change-Visibility", Name = "SyncPoint365-ChangeVisibility")]
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> UpdateVisibility([FromQuery] int id, CancellationToken cancellationToken)
        {
            var result = await _companyNewsService.UpdateVisibilityAsync(id, cancellationToken);
            if (!result)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpPost]
        [Route("add", Name = "SyncPoint365-AddCompanyNews")]
        [Authorize(Policy = "SuperAdminPolicy")]
        public async Task<IActionResult> AddCompanyNewsAsync([FromBody] CompanyNewsAddDTO dto, CancellationToken cancellationToken)
        {
            try
            {
                var userId = Auth.GetLoggedUserId(HttpContext.User);

                dto.UserId = userId;

                await _companyNewsService.AddAsync(dto, cancellationToken);

                return Ok();
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
