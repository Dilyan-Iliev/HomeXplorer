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

        public static string GetSubstringedName(this ClaimsPrincipal user)
        {
            string email = user.FindFirstValue(ClaimTypes.Email);

            int index = email.IndexOf("@");
            string substringedName = email.Substring(0, index);

            string firstLetter = substringedName.Substring(0, 1);
            string remainingLetters = substringedName.Substring(1);

            string transformedName = firstLetter.ToUpper() + remainingLetters;

            return transformedName;
        }
    }
}
