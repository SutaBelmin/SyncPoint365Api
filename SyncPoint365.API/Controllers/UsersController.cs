using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.API.RESTModels;
using SyncPoint365.Core.DTOs.Users;
using SyncPoint365.Core.Helpers;
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
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUsersListAsync(CancellationToken cancellationToken = default)
        {
            var data = await _usersService.GetUsersListAsync();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        [Route("Change-Status", Name = "SyncPoint365-ChangeStatus")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> ChangeUserStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var status = await _usersService.ChangeUserStatusAsync(id, cancellationToken);
                return Ok(new { IsActive = status });
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("Email-Exists", Name = "SyncPoint365-EmailExists")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<bool> EmailExists(string email)
        {

            return await _usersService.EmailExists(email);
        }

        [HttpGet]
        [Route("Paged", Name = "SyncPoint365-GetUsersPagedListAsync")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> GetUsersPagedListAsync(bool? isActive, string? query = null, int? roleId = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var data = await _usersService.GetUsersPagedListAsync(isActive, query, roleId, orderBy, page, pageSize, cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(GetPagedResult(data));
        }

        [HttpPut]
        [Route("Change-Password", Name = "SyncPoint365-ChangePassword")]
        [Authorize(Policy = "AdminEmployeePolicy")]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] UserChangePasswordModel model, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid input data." });
            }

            try
            {
                var result = await _usersService.ChangePasswordAsync(model.Id, model.Password, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost]
        [Route("Upload-Profile-Picture", Name = "SyncPoint365-UploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePictureAsync([FromForm] FileUploadRequest request, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usersService.UploadProfilePictureAsync(request, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        [Route("Get-Profile-Picture", Name = "SyncPoint365-GetProfilePicture")]
        public async Task<IActionResult> GetProfilePictureAsync(int userId, CancellationToken cancellationToken = default)
        {

            var user = await _usersService.GetByIdAsync(userId, cancellationToken);
            if (user == null)
            {
                return NotFound(new { error = "User not found." });
            }

            if (string.IsNullOrEmpty(user.ImagePath))
            {
                return NotFound(new { error = "Profile picture not available." });
            }

            var filePath = Path.Combine("wwwroot", user.ImagePath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound(new { error = "File not found on server." });
            }

            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath, cancellationToken);
            var contentType = "image/" + Path.GetExtension(user.ImagePath).TrimStart('.');

            return File(fileBytes, contentType);

        }

        [HttpDelete]
        [Route("Delete-Image", Name = "SyncPoint365-DeleteUserImage")]
        public async Task<IActionResult> DeleteUserImageAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                var result = await _usersService.DeleteUserImageAsync(id, cancellationToken);
                if (!result)
                {
                    return NotFound();
                }

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }


    }
}
