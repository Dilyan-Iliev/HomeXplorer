namespace HomeXplorer.Config.Cloudinary
{
    using CloudinaryDotNet;

    public class CloudinaryConfig
    {
        public static Cloudinary GetCloudinaryInstance(IConfiguration configuration)
        {
            Account account = new Account(
                configuration.GetValue<string>("Cloudinary:cloud_name"),
                configuration.GetValue<string>("Cloudinary:api_key"),
                configuration.GetValue<string>("Cloudinary:api_secret")
            );

            Cloudinary cloudinary = new Cloudinary(account);

            return cloudinary;
        }
    }
}
