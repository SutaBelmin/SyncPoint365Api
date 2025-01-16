namespace SyncPoint365.Core.Entities
{
    public class RefreshToken : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = default!;
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
    }
}
