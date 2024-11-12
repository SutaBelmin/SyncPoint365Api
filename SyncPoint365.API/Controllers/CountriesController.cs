using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountriesController : BaseController<CountriesDTO, CountriesAddDTO, CountriesUpdateDTO>
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService) : base(countriesService)
        {
            _countriesService = countriesService;
        }
    }
}
