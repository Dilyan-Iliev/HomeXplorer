namespace HomeXplorer.Common
{
    public static class DataConstants
    {
        public static class ApplicationUserConstants
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 5;
            public const int LastNameMaxLength = 20;

            public const string EmailRegex = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-]+)(\.[a-zA-Z]{2,5}){1,2}$";

            public const string PhoneNumberRegex = @"^(?:\+\d{12}|\d{10})$";

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 12;
        }

        public static class PropertyConstants
        {

        }
    }
}
