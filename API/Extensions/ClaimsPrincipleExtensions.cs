using System.Security.Claims;

namespace API.Extensions
{
    public static class ClaimsPrincipleExtensions
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        //use if username provided
        // public static string GetUsername(this ClaimsPrincipal user)
        // {
        //     return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        // }
    }
}