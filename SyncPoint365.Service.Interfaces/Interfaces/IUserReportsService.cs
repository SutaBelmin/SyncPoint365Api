using SyncPoint365.Core.DTOs.UserReports;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface IUserReportsService
    {
        Task<UserReportDTO> GenerateUserReportAsync(int userId);
    }
}
