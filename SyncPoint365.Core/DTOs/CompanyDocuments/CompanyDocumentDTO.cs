using SyncPoint365.Core.DTOs.Users;

namespace SyncPoint365.Core.DTOs.CompanyDocuments
{
    public class CompanyDocumentDTO : BaseDTO
    {
        public string Name { get; set; } = default!;
        public byte[] File { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
        public UserDTO User { get; set; } = default!;
    }
}
