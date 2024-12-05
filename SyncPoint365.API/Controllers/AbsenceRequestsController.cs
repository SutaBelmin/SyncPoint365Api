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
        [Route("paged", Name = "SyncPoint365-GetAbsenceRequestsPagedListAsync")]
        public async Task<IActionResult> GetAbsenceRequestTypesPagedListAsync(int? absenceRequestTypeId = null, int? userId = null, DateTime? dateFrom = null, DateTime? dateTo = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var items = await _absenceRequestsService.GetAbsenceRequestsPagedListAsync(absenceRequestTypeId, userId, dateFrom, dateTo, page, pageSize, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }

    }

}
