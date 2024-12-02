using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Enums;
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
            var roles = _enumsService.GetRoles();

            if (roles == null)
            {
                return NotFound("Roles not found.");
            }

            return Ok(roles);
        }

    }
}
