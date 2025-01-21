namespace SyncPoint365.API.RESTModels
{
    public class AuthTokenValidationModel
    {
        public int UserId { get; set; }
        public string RefreshToken { get; set; } = default!;
    }
}
