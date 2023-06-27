namespace HomeXplorer.Config.Cloudinary
{
    using CloudinaryDotNet;

    public class CloudinaryConfig
    {
        private readonly IConfiguration configuration;
        private readonly Cloudinary cloudinary;

        public CloudinaryConfig(IConfiguration configuration)
        {
            this.configuration = configuration;

            Account account = new Account(
                this.configuration.GetValue<string>("Cloudinary:cloud_name"),
                this.configuration.GetValue<string>("Cloudinary:api_key"),
                this.configuration.GetValue<string>("Cloudinary:api_secret")
            );

            cloudinary = new Cloudinary(account);
        }

        public Cloudinary GetCloudinaryInstance()
        {
            return cloudinary;
        }
    }
}
