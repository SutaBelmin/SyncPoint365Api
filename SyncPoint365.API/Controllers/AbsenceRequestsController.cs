using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.AbsenceRequests;
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

    }

}
