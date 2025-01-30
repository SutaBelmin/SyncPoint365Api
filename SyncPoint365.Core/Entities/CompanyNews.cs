namespace SyncPoint365.Core.Entities
{
    public class CompanyNews : BaseEntity
    {
        public string Title { get; set; } = default!;
        public string Text { get; set; } = default!;
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
