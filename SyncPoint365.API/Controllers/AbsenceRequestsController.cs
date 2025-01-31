using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.API.Helpers;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.DTOs.AbsenceRequests;
using SyncPoint365.Core.Enums;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;


namespace SyncPoint365.API.Controllers
{
    [Authorize(Policy = "AdminEmployeePolicy")]
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
        [Route("list", Name = "SyncPoint365-GetAbsenceRequestsListAsync")]
        public async Task<IActionResult> GetAbsenceRequestListAsync(int? userId, DateTime? dateFrom, DateTime? dateTo, CancellationToken cancellationToken = default)
        {
            var loggedUser = HttpContext.User;
            var loggedUserRole = Auth.GetLoggedUserRole(loggedUser);
            var loggedUserId = Auth.GetLoggedUserId(loggedUser);

            if (loggedUserRole == Role.Employee.ToString())
            {
                userId = loggedUserId;
            }

            var items = await _absenceRequestsService.GetAbsenceRequestListAsync(userId, dateFrom, dateTo, cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet]
        [Route("paged", Name = "SyncPoint365-GetAbsenceRequestsPagedListAsync")]
        public async Task<IActionResult> GetAbsenceRequestTypesPagedListAsync(int? absenceRequestTypeId = null, int? userId = null, int? absenceRequestStatusId = null, DateTime? dateFrom = null, DateTime? dateTo = null,
            int? year = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var loggedUser = HttpContext.User;
            var loggedUserRole = Auth.GetLoggedUserRole(loggedUser);
            var loggedUserId = Auth.GetLoggedUserId(loggedUser);

            if (loggedUserRole == Role.Employee.ToString())
            {
                userId = loggedUserId;
            }

            var items = await _absenceRequestsService.GetAbsenceRequestsPagedListAsync(absenceRequestTypeId, userId, absenceRequestStatusId, dateFrom, dateTo, year, orderBy, page, pageSize, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }


        [HttpPut]
        [Route("Change-Absence-Request-Status", Name = "SyncPoint365-ChangeAbsenceRequestStatus")]
        public async Task<IActionResult> ChangeAbsenceRequestStatusAsync([FromBody] AbsenceRequestStatusChangeModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid input data." });
            }
            try
            {
                var updatedStatus = await _absenceRequestsService.ChangeAbsenceRequestStatusAsync(model.Id, model.Status, model.PostComment, cancellationToken);
                return Ok(new { AbsenceRequestStatus = updatedStatus });
            }
            catch
            {
                return BadRequest("Can't change status.");
            }
        }

    }

}
