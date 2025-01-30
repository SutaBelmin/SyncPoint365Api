using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Reports;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserReportsController : ControllerBase
    {
        private readonly IUserReportsService _usersReportService;

        public UserReportsController(IUserReportsService userReportsService)
        {
            _usersReportService = userReportsService;
        }

        [HttpGet]
        [Route("Generate-User-Report", Name = "SyncPoint365-GenerateUserReport")]
        public async Task<IActionResult> GenerateUserReportAsync(int userId)
        {
            var reportData = await _usersReportService.GenerateUserReportAsync(userId);
            var report = new UserReport();

            using (MemoryStream ms = new MemoryStream())
            {
                report.ExportToPdf(ms);
                return File(ms.ToArray(), "application/pdf", "UserReport.pdf");
            }
        }
    }
}
