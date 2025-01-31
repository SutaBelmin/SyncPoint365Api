using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Service.Common.Interfaces;

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
    }
}
