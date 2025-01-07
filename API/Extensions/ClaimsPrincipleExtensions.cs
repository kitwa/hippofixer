using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.Email)?.Value;
        }

        public static int GetUserId(this ClaimsPrincipal user)
        {
            // Extract the NameIdentifier claim (commonly used for user ID) and parse it as an integer
            return int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        // Uncomment if you want to use username instead
        // public static string GetUsername(this ClaimsPrincipal user)
        // {
        //     return user.FindFirst(ClaimTypes.Name)?.Value;
        // }
    }
}
