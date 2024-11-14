namespace SyncPoint365.Core.Entities
{
    public class City : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public int? PostalCode { get; set; }
        public int CountryId { get; set; }
        public virtual Country Country { get; set; } = default!;

    }
}
