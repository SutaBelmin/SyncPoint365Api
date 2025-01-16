using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.CompanyNews;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("company-news")]
    [ApiController]
    public class CompanyNewsController : BaseController<CompanyNewsDTO, CompanyNewsAddDTO, CompanyNewsUpdateDTO>
    {
        protected ICompanyNewsService _companyNewsService;
        public CompanyNewsController(ICompanyNewsService service) : base(service)
        {
            _companyNewsService = service;
        }
    }
}
