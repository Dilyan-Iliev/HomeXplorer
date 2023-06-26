namespace HomeXplorer.Services.Interfaces
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using HomeXplorer.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;

    public class CloudinaryService
        : ICloudinaryService
    {
        public async Task<ICollection<string>> UploadMany(Cloudinary cloudinary, ICollection<IFormFile> files)
        {
            ICollection<string> resultURIs = new List<string>();

            foreach (var file in files
                .Where(f => f.Length > 0))
            {
                byte[] imageBytes;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(imageBytes))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, destinationStream),
                    };

                    var uploadResult = await cloudinary.UploadAsync(uploadParams);

                    resultURIs.Add(uploadResult.Uri.AbsoluteUri);
                }
            }

            return resultURIs;
        }
        
        public async Task<string> UploadSingle(Cloudinary cloudinary, IFormFile file)
        {
            string resultURI = string.Empty;

            if (file.Length > 0)
            {
                byte[] imageBytes;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(imageBytes))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, destinationStream),
                    };

                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    resultURI = uploadResult.Uri.AbsoluteUri;
                }
            }

            return resultURI;
        }
    }
}