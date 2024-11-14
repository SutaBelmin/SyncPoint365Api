namespace SyncPoint365.Core.Entities
{
    public class Country : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string DisplayName { get; set; } = default!;

        public virtual ICollection<City> Cities { get; set; } = default!;

    }
}
