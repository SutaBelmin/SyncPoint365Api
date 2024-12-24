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
        public Gender Gender { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Role Role { get; set; }
        public bool IsActive { get; set; } = default;
        public int CityId { get; set; }
        public virtual City City { get; set; } = default!;
        public string? ImagePath { get; set; }
    }
}
