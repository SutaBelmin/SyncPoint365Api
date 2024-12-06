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
        public async Task<IActionResult> UpdateUserStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var status = await _usersService.UpdateUserStatusAsync(id, cancellationToken);
                string message = status ? "User activated successfully!" : "User deactivated successfully!";
                return Ok(new { Message = message });
            }
            catch (Exception ex)
            {

                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
