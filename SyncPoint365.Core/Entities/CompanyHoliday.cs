namespace SyncPoint365.Core.Entities
{
    public class CompanyHoliday : BaseEntity
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
