using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.CompanyHolidays;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CompanyHolidaysController : BaseController<CompanyHolidayDTO, CompanyHolidayAddDTO, CompanyHolidayUpdateDTO>
    {
        private readonly ICompanyHolidaysService _companyHolidaysService;
        public CompanyHolidaysController(ICompanyHolidaysService companyHolidaysService) : base(companyHolidaysService)
        {
            _companyHolidaysService = companyHolidaysService;
        }
    }
}
