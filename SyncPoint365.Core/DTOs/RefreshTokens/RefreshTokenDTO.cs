namespace SyncPoint365.Core.DTOs.RefreshTokens
{
    public class RefreshTokenDTO : BaseDTO
    {
        public string Token { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
    }
}
