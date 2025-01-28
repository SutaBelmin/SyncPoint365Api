using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.Users
{
    public class UserAuthDTO
    {
        public string FullName { get; set; } = default!;
        public Role Role { get; set; }

    }
}
