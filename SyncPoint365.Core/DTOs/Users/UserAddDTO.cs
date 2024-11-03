using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.Users
{
    public class UserAddDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public Role Role { get; set; }
    }
}
