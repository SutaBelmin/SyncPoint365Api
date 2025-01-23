namespace SyncPoint365.Core.DTOs.RefreshTokens
{
    public class RefreshTokenAddDTO : BaseAddDTO
    {
        public int UserId { get; set; }
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
    }
}
