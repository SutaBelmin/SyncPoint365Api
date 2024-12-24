using Microsoft.AspNetCore.Http;

namespace SyncPoint365.Core.DTOs.Users
{
    public class FileUploadRequest
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; } = null!;
    }
}
