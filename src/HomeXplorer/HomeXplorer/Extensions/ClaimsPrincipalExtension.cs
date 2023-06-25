namespace HomeXplorer.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtension
    {
        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
