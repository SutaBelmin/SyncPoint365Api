namespace SyncPoint365.Core.DTOs.CompanyNews
{
    public class CompanyNewsUpdateDTO : BaseUpdateDTO
    {
        public string Title { get; set; } = default!;
        public string Text { get; set; } = default!;
        public bool IsVisible { get; set; }
        public int UserId { get; set; }
    }
}
