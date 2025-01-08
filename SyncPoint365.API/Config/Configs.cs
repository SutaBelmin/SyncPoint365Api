namespace SyncPoint365.API.Config
{
    public class ConnectionStrings
    {
        public string Main { get; set; } = default!;
    }

    public class JWTSettings
    {
        public int Duration { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public string Key { get; set; } = default!;
    }
    public class FileSettings
    {
        public List<string> AllowedExtensions { get; set; } = default!;
        public string UserImagesPath { get; set; } = default!;
    }
}
