namespace SyncPoint365.Core.DTOs
{
    public abstract class BaseDTO
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
