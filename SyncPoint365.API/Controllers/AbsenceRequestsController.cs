using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;


namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AbsenceRequestsController : BaseController<AbsenceRequestDTO, AbsenceRequestAddDTO, AbsenceRequestUpdateDTO>
    {
        protected IAbsenceRequestsService _absenceRequestsService;
        public AbsenceRequestsController(IAbsenceRequestsService service) : base(service)
        {
            _absenceRequestsService = service;
        }


        [HttpGet]
        [Route("list", Name = "SyncPoint365-GetAbsenceRequestsList\"")]
        public async Task<IActionResult> GetAbsenceRequestsListAsync(CancellationToken cancellationToken = default)
        {
            var items = await _absenceRequestsService.GetAbsenceRequestsListAsync(cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet]
        [Route("paged", Name = "SyncPoint365-GetAbsenceRequestsPagedListAsync")]
        public async Task<IActionResult> GetAbsenceRequestTypesPagedListAsync(string? nameQuery = null, string? typeQuery = null, DateTime dateFrom = default, DateTime dateTo = default, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            dateFrom = dateFrom == default ? DateTime.Today : dateFrom;
            dateTo = dateTo == default ? new DateTime(DateTime.Today.Year, 12, 31) : dateTo;
            var items = await _absenceRequestsService.GetAbsenceRequestsPagedListAsync(nameQuery, typeQuery, dateFrom, dateTo, page, pageSize, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }

    }

}
