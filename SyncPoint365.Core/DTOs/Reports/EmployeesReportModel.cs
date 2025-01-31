using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Core.DTOs.Reports
{
    public class EmployeesReportModel
    {
        public EmployeeReportHeaderModel EmployeeReportHeader { get; set; }
        public List<UserDTO> Employees { get; set; }
    }

    public class EmployeeReportHeaderModel
    {
        public DateTime DateCreated { get; set; }
    }
}
