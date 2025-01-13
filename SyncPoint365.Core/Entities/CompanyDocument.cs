namespace SyncPoint365.Core.Entities
{
    public class CompanyDocument : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string FileName { get; set; } = default!;
        public string FilePath { get; set; } = default!;
        public long FileSize { get; set; }
        public string Extension { get; set; } = default!;
        public string ContentType { get; set; } = default!;
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
