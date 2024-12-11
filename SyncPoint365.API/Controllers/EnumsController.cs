using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Enums;
using SyncPoint365.Core.Enums;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        private readonly IEnumsService _enumsService;

        public EnumsController(IEnumsService enumsService)
        {
            this._enumsService = enumsService;
        }


        [HttpGet]
        [Route("Roles", Name = "SyncPoint365-GetRoles")]
        public ActionResult<IEnumerable<SelectItemDTO>> GetRoles()
        {
            var roles = _enumsService.GetEnumValues<Role>();

            if (roles == null)
            {
                return NotFound("Roles not found.");
            }

            return Ok(roles);
        }

        [HttpGet]
        [Route("Genders", Name = "SyncPoint365-GetGenders")]
        public ActionResult<IEnumerable<SelectItemDTO>> GetGenders()
        {
            var genders = _enumsService.GetEnumValues<Gender>();

            if (genders == null)
            {
                return NotFound("Genders not found.");
            }

            return Ok(genders);
        }

        [HttpGet]
        [Route("absence-requests-status", Name = "SyncPoint365-GetAbsenceRequestsStatus")]
        public ActionResult<IEnumerable<SelectItemDTO>> GetAbsenceRequestsStatus()
        {
            var absenceRequestStatus = _enumsService.GetEnumValues<AbsenceRequestStatus>();

            if (absenceRequestStatus == null)
            {
                return NotFound("Absence request status not found.");
            }

            return Ok(absenceRequestStatus);
        }

    }
}
