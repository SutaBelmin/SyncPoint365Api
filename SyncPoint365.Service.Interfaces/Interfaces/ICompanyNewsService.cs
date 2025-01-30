using SyncPoint365.Core.DTOs.CompanyNews;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICompanyNewsService : IBaseService<CompanyNewsDTO, CompanyNewsAddDTO, CompanyNewsUpdateDTO>
    {
        Task<IPagedList<CompanyNewsDTO>> GetCompanyNewsPagedListAsync(string? query, bool? isVisible, DateTime? dateFrom, DateTime? dateTo, string? orderBy, int page, int pageSize, CancellationToken cancellationToken);
        Task<bool> UpdateVisibilityAsync(int id, CancellationToken cancellationToken);
    }
}
