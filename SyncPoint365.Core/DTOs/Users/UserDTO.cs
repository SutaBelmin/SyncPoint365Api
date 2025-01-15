using SyncPoint365.Core.Enums;

namespace SyncPoint365.Core.DTOs.Users
{
    public class UserDTO : BaseDTO
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public Gender Gender { get; set; } = default!;
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; } = default!;
        public string Address { get; set; } = default!;
        public Role Role { get; set; }
        public bool isActive { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; } = default!;
        public string? ImageContent { get; set; }
    }
}
