using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Cities;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Authorize(Policy = "SuperAdminPolicy")]
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
        [Route("List", Name = "SyncPoint365-GetCitiesList")]
        public async Task<IActionResult> GetCitiesListAsync(CancellationToken cancellationToken = default)
        {
            var data = await _citiesService.GetCitiesListAsync();

            if (data == null)
                return NotFound();

            return Ok(data);
        }


        [HttpGet]
        [Route("Paged", Name = "SyncPoint365-GetCitiesPaged")]
        public async Task<IActionResult> GetPagedCitiesAsync(int? countryId = null, string? query = null, string? orderBy = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var data = await _citiesService.GetPagedCitiesAsync(countryId, query, orderBy, page, pageSize, cancellationToken);

            if (data == null)
                return NotFound();

            return Ok(GetPagedResult(data));
        }

    }
}
