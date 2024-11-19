using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Service.Common.Interfaces;

namespace SyncPoint365.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountriesController : BaseController<CountryDTO, CountryAddDTO, CountryUpdateDTO>
    {
        private readonly ICountriesService _countriesService;

        public CountriesController(ICountriesService countriesService) : base(countriesService)
        {
            _countriesService = countriesService;
        }

        [HttpGet]
        [Route("List", Name = "SyncPoint365-GetCountriesList")]
        public async Task<IActionResult> GetCountriesListAsync(CancellationToken cancellationToken = default)
        {
            var data = await _countriesService.GetCountriesListAsync();

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpGet]
        [Route("Search", Name = "SyncPoint365-SearchCountriesByName")]
        public async Task<IActionResult> SearchCountriesByNameAsync([FromQuery] string name, CancellationToken cancellationToken = default)
        {
            var data = await _countriesService.SearchCountriesByNameAsync(name, cancellationToken);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        //[HttpGet("Paged/{page}")]
        //public async Task<IActionResult> GetPagedListAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        //{
        //    var pagedList = await _countriesService.GetAsync(query, page, pageSize, cancellationToken);
        //    return Ok(GetPagedResult(pagedList));
        //}
    }
}
