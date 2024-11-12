namespace SyncPoint365.Core.Entities
{
    public class AbsenceRequestType : BaseEntity
    {
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; }
    }
}
