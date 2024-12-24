using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Enums;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;


namespace SyncPoint365.API.Controllers
{
    [Route("absence-requests")]
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
        public async Task<IActionResult> GetAbsenceRequestTypesPagedListAsync(int? absenceRequestTypeId = null, int? userId = null, int? absenceRequestStatusId = null, DateTime? dateFrom = null, DateTime? dateTo = null,
            string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {

            var items = await _absenceRequestsService.GetAbsenceRequestsPagedListAsync(absenceRequestTypeId, userId, absenceRequestStatusId, dateFrom, dateTo, orderBy, page, pageSize, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }

        [HttpPut]
        [Route("change-absence-request-status", Name = "SyncPoint365-ChangeAbsenceRequestStatus")]
        public async Task<IActionResult> ChangeAbsenceRequestStatusAsync(int id, [FromBody] AbsenceRequestStatus newStatus, CancellationToken cancellationToken = default)
        {
            try
            {
                var updatedStatus = await _absenceRequestsService.ChangeAbsenceRequestStatusAsync(id, newStatus, cancellationToken);
                return Ok(new { AbsenceRequestStatus = updatedStatus });
            }
            catch
            {
                return BadRequest("Can't change status.");
            }
        }

    }

}
