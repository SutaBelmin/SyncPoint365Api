using Microsoft.AspNetCore.Http;
using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.Users
{
    public class UserAddDTO : BaseAddDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Role Role { get; set; }
        public int CityId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
