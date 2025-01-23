using Microsoft.AspNetCore.Http;

namespace SyncPoint365.Service.Helpers
{
    public static class FileHelper
    {
        public static byte[] GetFileBytes(IFormFile file)
        {
            if (file == null)
                return Array.Empty<byte>();
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
