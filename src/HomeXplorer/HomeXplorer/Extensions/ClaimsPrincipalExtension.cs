namespace HomeXplorer.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtension
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static string GetName(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.Email);
        }
    }
}
