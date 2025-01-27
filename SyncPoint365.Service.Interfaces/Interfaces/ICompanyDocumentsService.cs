using SyncPoint365.Core.DTOs.CompanyDocuments;
using SyncPoint365.Core.Helpers;
using X.PagedList;

namespace SyncPoint365.Service.Common.Interfaces
{
    public interface ICompanyDocumentsService : IBaseService<CompanyDocumentDTO, CompanyDocumentAddDTO, CompanyDocumentUpdateDTO>
    {
        Task<IPagedList<CompanyDocumentDTO>> GetPagedCompanyDocumentsAsync(DateTime? dateFrom, DateTime? dateTo, string? query = null, bool? isVisible = null, int page = Constants.Pagination.PageNumber, int pageSize = Constants.Pagination.PageSize, CancellationToken cancellationToken = default);
        Task<bool> UpdateCompanyDocumentVisibiltyAsync(int id, CancellationToken cancellationToken = default);
    }
}
