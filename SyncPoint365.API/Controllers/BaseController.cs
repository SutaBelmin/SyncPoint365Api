using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs;
using SyncPoint365.Service.Common.Interfaces;


namespace SyncPoint365.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TDTO, TAddDTO> : ControllerBase
        where TDTO : BaseDTO
    {
        protected readonly IBaseService<TDTO> Service;

        public BaseController(IBaseService<TDTO> service)
        {
            Service = service;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(int? id, CancellationToken cancellationToken = default)
        {
            if (id == null || id == default(int))
                return NotFound();

            var data = await Service.GetByIdAsync(id.Value, cancellationToken);
            if (data == null)
                return NotFound();

            return Ok(data);
        }
    }
}
