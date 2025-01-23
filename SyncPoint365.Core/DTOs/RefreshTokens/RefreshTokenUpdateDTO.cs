namespace SyncPoint365.Core.DTOs.RefreshTokens
{
    public class RefreshTokenUpdateDTO : BaseUpdateDTO
    {
        public int UserId { get; set; }
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
    }
}
