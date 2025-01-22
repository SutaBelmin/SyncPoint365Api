using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SyncPoint365.Core.DTOs;
using SyncPoint365.Core.Helpers;
using SyncPoint365.Service.Common.Interfaces;
using X.PagedList;

namespace SyncPoint365.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TDTO, TAddDTO, TUpdateDTO> : ControllerBase
        where TDTO : BaseDTO
        where TAddDTO : BaseAddDTO
        where TUpdateDTO : BaseUpdateDTO
    {
        protected readonly IBaseService<TDTO, TAddDTO, TUpdateDTO> Service;

        public BaseController(IBaseService<TDTO, TAddDTO, TUpdateDTO> service)
        {
            Service = service;
        }

        [HttpGet("Paged-List")]
        public virtual async Task<IActionResult> GetPagedListAsync(string? query = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default)
        {
            var pagedList = await Service.GetAsync(query, page, pageSize, cancellationToken: cancellationToken);

            return Ok(GetPagedResult(pagedList));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetByIdAsync(int? id, CancellationToken cancellationToken = default)
        {
            if (id == null || id == default(int))
                return NotFound();

            var data = await Service.GetByIdAsync(id.Value, cancellationToken);
            if (data == null)
                return NotFound();

            return Ok(data);
        }

        [HttpPost]
        public virtual async Task<IActionResult> AddAsync(TAddDTO dto, CancellationToken cancellationToken = default)
        {
            try
            {
                await Service.AddAsync(dto, cancellationToken);

                return Ok();
            }
            catch (ValidationException exception)
            {
                return BadRequest(GetValidationErrors(exception));
            }
            catch (Exception)
            {
                return BadRequest("There was something wrong. Sorry about that!");
            }
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(TUpdateDTO dto, CancellationToken cancellationToken = default)
        {
            try
            {
                await Service.UpdateAsync(dto, cancellationToken);

                return Ok();
            }
            catch (ValidationException exception)
            {
                return BadRequest(GetValidationErrors(exception));
            }
            catch (Exception)
            {
                return BadRequest("There was something wrong. Sorry about that!");
            }
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(int? id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id == null || id == default(int))
                    return NotFound();

                await Service.DeleteAsync(id.Value, cancellationToken);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest("There was something wrong. Sorry about that!");
            }
        }

        [NonAction]
        protected PagedResult<TDTO> GetPagedResult(IPagedList<TDTO> pagedList)
        {
            return new PagedResult<TDTO>
            {
                PageSize = pagedList.PageSize,
                PageCount = pagedList.PageCount,
                HasNextPage = pagedList.HasNextPage,
                HasPreviousPage = pagedList.HasPreviousPage,
                TotalItemCount = pagedList.TotalItemCount,
                Items = pagedList.ToList()
            };
        }

        [NonAction]
        protected List<string> GetValidationErrors(ValidationException exception)
        {
            return exception.Errors.Select(e => e.ErrorMessage).ToList();
        }
    }

    public class PagedResult<T>
    {
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public int TotalItemCount { get; set; }
        public List<T> Items { get; set; } = default!;
    }
}
