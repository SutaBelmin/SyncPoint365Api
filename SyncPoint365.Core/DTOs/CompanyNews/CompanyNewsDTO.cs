using SyncPoint365.Core.Entities;

namespace SyncPoint365.Core.DTOs.CompanyNews
{
    public class CompanyNewsDTO : BaseDTO
    {
        public string Title { get; set; } = default!;
        public string Text { get; set; } = default!;
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
