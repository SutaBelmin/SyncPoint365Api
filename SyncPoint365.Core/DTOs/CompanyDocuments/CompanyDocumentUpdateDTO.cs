using Microsoft.AspNetCore.Http;

namespace SyncPoint365.Core.DTOs.CompanyDocuments
{
    public class CompanyDocumentUpdateDTO : BaseUpdateDTO
    {
        public string Name { get; set; } = default!;
        public IFormFile? File { get; set; } = default!;
    }
}
