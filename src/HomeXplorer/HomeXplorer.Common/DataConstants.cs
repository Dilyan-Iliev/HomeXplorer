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

            public const string DefaultUserProfilePictureUrl =
                "https://res.cloudinary.com/degtesnvc/image/upload/v1688283726/default-avatar-profile-icon-of-social-media-user-vector_lcoi8s.jpg";
        }

        public static class PropertyConstants
        {
            public const int NameMinLength = 10;
            public const int NameMaxLength = 30;

            public const int DescriptionMinlength = 20;
            public const int DescriptionMaxLength = 100;

            public const int AddressMinLength = 15;
            public const int AddressMaxLength = 80;

            public const decimal MinPrie = 250m;
            public const decimal MaxPrice = 100_000m;

            public const int MinSize = 40;
            public const int MaxSize = 500;
        }
    }
}
