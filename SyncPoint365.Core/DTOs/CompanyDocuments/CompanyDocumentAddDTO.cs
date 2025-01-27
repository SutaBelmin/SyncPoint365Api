using Microsoft.AspNetCore.Http;

namespace SyncPoint365.Core.DTOs.CompanyDocuments
{
    public class CompanyDocumentAddDTO : BaseAddDTO
    {
        public string Name { get; set; } = default!;
        public IFormFile File { get; set; } = default!;
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
    }
}
