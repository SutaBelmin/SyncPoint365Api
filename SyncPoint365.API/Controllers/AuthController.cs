using Microsoft.AspNetCore.Mvc;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public AuthController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost]
        [Route("Login", Name = "SyncPoint365-Login")]
        public async Task<IActionResult> Login([FromBody] AuthModel model, CancellationToken cancellationToken = default)
        {
            //toDo
            return Ok();
        }
    }
}
