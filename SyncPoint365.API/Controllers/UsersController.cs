using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : BaseController<UserDTO, UserAddDTO, UserUpdateDTO>
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService) : base(usersService)
        {
            this._usersService = usersService;
        }


        [HttpGet]
        [Route("Get-Users", Name = "SyncPoint365-GetUsers")]
        public async Task<IActionResult> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            var data = await _usersService.GetUsersListAsync();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        [Route("Deactivate-Users", Name = "SyncPoint365-DeactivateUsers")]
        public async Task<IActionResult> DeactivateUserAsync([FromBody] UserDeactivateDTO userDeactivateDTO, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                await _usersService.ActivateDeactivateUserAsync(userDeactivateDTO, cancellationToken);

                if (userDeactivateDTO.isActive == true)
                {
                    return Ok(new { Message = "User activated successfully!" });
                }
                else
                {
                    return Ok(new { Message = "User deactivated successfully!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
