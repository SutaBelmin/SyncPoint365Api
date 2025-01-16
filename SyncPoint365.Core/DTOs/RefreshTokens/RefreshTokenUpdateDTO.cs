namespace SyncPoint365.Core.DTOs.RefreshTokens
{
    public class RefreshTokenUpdateDTO : BaseUpdateDTO
    {
        public string RefreshToken { get; set; } = default!;
        public DateTime ExpirationDate { get; set; }
    }
}
