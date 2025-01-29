using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.Users
{
    public class UserLoginDTO : BaseDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Role Role { get; set; }
        public bool IsActive { get; set; }
        public string PasswordHash { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
    }
}
