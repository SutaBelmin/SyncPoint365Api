using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.AbsenceRequestTypes;
using SyncPoint365.Service.Common.Interfaces;
using Constants = SyncPoint365.Core.Helpers.Constants;
namespace SyncPoint365.API.Controllers
{
    [Route("absence-request-types")]
    [ApiController]
    public class AbsenceRequestTypesController : BaseController<AbsenceRequestTypeDTO, AbsenceRequestTypeAddDTO, AbsenceRequestTypeUpdateDTO>
    {
        protected readonly IAbsenceRequestTypesService _absenceRequestTypeService;
        public AbsenceRequestTypesController(IAbsenceRequestTypesService service) : base(service)
        {
            _absenceRequestTypeService = service;
        }

        [HttpGet]
        [Route("list", Name = "SyncPoint365-GetAbsenceRequestTypesList")]
        public async Task<IActionResult> GetAbsenceRequestTypesListAsync(bool? isActive = null, CancellationToken cancellationToken = default)
        {
            var items = await _absenceRequestTypeService.GetAbsenceRequestTypesListAsync(isActive);

            if (items == null)
                return NotFound();

            return Ok(items);
        }

        [HttpGet]
        [Route("paged", Name = "SyncPoint365-GetAbsenceRequestTypesPagedListAsync")]
        public async Task<IActionResult> GetAbsenceRequestTypesPagedListAsync(bool? isActive = null, string? query = null,
            int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, string? sortOrder = null, CancellationToken cancellationToken = default)
        {
            var items = await _absenceRequestTypeService.GetAbsenceRequestTypesPagedListAsync(isActive, query, page, pageSize, sortOrder, cancellationToken: cancellationToken);

            if (items == null)
                return NotFound();

            return Ok(GetPagedResult(items));
        }
    }
}