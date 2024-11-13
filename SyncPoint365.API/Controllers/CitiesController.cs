using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Service.Common.Interfaces;
using SyncPoint365.Service.Services;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CitiesController : BaseController<CityDTO, CityAddDTO, CityUpdateDTO>
    {
        private readonly ICitiesService _citiesService;
        public CitiesController(ICitiesService citiesService) : base(citiesService)
        {
            _citiesService = citiesService;
        }


        [HttpGet]
        [Route("Get-Cities", Name = "SyncPoint365-GetCities")]
        public async Task<IActionResult> GetCitiesListAsync(CancellationToken cancellationToken = default)
        {
            var data = await _citiesService.GetCitiesListAsync();

            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
}
