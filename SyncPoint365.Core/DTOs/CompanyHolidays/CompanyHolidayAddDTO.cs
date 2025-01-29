namespace SyncPoint365.Core.DTOs.CompanyHolidays
{
    public class CompanyHolidayAddDTO : BaseAddDTO
    {
        public string Name { get; set; } = default!;
        public DateTime Date { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
    }
}
