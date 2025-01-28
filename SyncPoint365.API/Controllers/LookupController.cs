using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ICitiesService _citiesService;
        private readonly ICountriesService _countriesService;
        private readonly IAbsenceRequestTypesService _absenceRequestTypeService;

        public LookupController(ICitiesService citiesService, ICountriesService countriesService, IAbsenceRequestTypesService absenceRequestTypeService)
        {
            _citiesService = citiesService;
            _countriesService = countriesService;
            _absenceRequestTypeService = absenceRequestTypeService;
        }



        [HttpGet]
        [Route("Cities", Name = "SyncPoint365-GetCities")]
        public async Task<IActionResult> GetCitiesAsync(CancellationToken cancellationToken = default)
        {
            var data = await _citiesService.GetCitiesListAsync(cancellationToken);


            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet]
        [Route("Countries", Name = "SyncPoint365-GetCountries")]
        public async Task<IActionResult> GetCountriesAsync(CancellationToken cancellationToken = default)
        {
            var data = await _countriesService.GetCountriesListAsync(cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet]
        [Route("AbsenceRequestTypes", Name = "SyncPoint365-GetAbsenceRequestTypes")]
        public async Task<IActionResult> GetAbsenceRequestTypesAsync(CancellationToken cancellationToken = default)
        {
            var data = await _absenceRequestTypeService.GetAbsenceRequestTypesListAsync(true, cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
}
