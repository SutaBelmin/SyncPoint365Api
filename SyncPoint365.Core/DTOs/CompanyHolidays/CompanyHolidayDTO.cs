using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Core.DTOs.CompanyHolidays
{
    public class CompanyHolidayDTO : BaseDTO
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; } = default!;
    }
}
