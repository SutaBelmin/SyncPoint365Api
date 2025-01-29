namespace SyncPoint365.API.RESTModels
{
    public class RefreshTokenModel
    {
        public string RefreshToken { get; set; } = default!;
        public DateTime Expiration { get; set; } = default!;
    }
}
