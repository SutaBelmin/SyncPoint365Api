using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : BaseController<CountriesDTO, CountriesAddDTO, CountriesUpdateDTO>
    {
        public CountriesController(IBaseService<CountriesDTO, CountriesAddDTO, CountriesUpdateDTO> service) : base(service)
        {

        }
    }
}
