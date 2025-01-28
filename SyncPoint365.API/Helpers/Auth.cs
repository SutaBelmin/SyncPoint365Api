using System.Security.Claims;

namespace SyncPoint365.API.Helpers
{
    public static class Auth
    {
        public static int GetLoggedUserId(ClaimsPrincipal user)
        {
            if (user == null || !user.HasClaim(c => c.Type == ClaimTypes.NameIdentifier))
                throw new UnauthorizedAccessException("Logged user not found or does not have an ID!");

            return int.Parse(user.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
        }

        public static string GetLoggedUserRole(ClaimsPrincipal user)
        {
            if (user == null || !user.HasClaim(c => c.Type == ClaimTypes.Role))
                throw new UnauthorizedAccessException("Logged user not found or does not have a role!");

            return user.Claims.First(c => c.Type == ClaimTypes.Role).Value;
        }

    }
}
