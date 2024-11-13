namespace SyncPoint365.Core.DTOs.Countries
{
    public class CountryAddDTO : BaseAddDTO
    {
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
    }
}
