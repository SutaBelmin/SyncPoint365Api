using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Core.DTOs.UserReports
{
    public class UserReportDTO
    {
        public string Title { get; set; } = default!;
        public int UserId { get; set; } = default!;
        public UserDTO User { get; set; } = default!;
    }
    public class EmployeesReportModel
    {
        public List<UserReportDTO> Employees { get; set; } = default!;
    }
}
