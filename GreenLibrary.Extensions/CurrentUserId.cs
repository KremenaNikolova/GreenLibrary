namespace GreenLibrary.Extensions
{
    using System.Security.Claims;

    public static class CurrentUserId
    {
        public static string? GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
