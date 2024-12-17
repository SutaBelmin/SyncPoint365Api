using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs.Countries;
using SyncPoint365.Core.Helpers;
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
        [Route("Paged", Name = "SyncPoint365-GetCountriesPaged")]
        public async Task<IActionResult> GetPagedCountriesAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, string? orderBy = null, CancellationToken cancellationToken = default)
        {
            var data = await _countriesService.GetPagedCountriesAsync(query, page, pageSize, orderBy, cancellationToken);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(GetPagedResult(data));
        }
    }
}
