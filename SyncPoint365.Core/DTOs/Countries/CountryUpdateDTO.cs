namespace SyncPoint365.Core.DTOs.Countries
{
    public class CountryUpdateDTO : BaseUpdateDTO
    {
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
