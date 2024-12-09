using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string PasswordSalt { get; set; } = default!;
        public Role Role { get; set; }
        public bool isActive { get; set; } = default;
    }
}
