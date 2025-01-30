namespace SyncPoint365.Core.DTOs.CompanyHolidays
{
    public class CompanyHolidayUpdateDTO : BaseUpdateDTO
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
