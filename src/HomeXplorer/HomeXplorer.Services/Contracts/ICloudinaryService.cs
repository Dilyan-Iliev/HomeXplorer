namespace HomeXplorer.Services.Contracts
{
    using CloudinaryDotNet;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<ICollection<string>> UploadMany(Cloudinary cloudinary, ICollection<IFormFile> files);

        Task<string> UploadSingle(Cloudinary cloudinary, IFormFile file);
    }
}
