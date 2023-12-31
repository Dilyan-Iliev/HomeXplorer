﻿namespace HomeXplorer.Services.Contracts
{
    using Microsoft.AspNetCore.Http;

    using CloudinaryDotNet;

    public interface ICloudinaryService
    {
        Task<ICollection<string>> UploadMany(Cloudinary cloudinary, ICollection<IFormFile> files);

        Task<string> UploadSingle(Cloudinary cloudinary, IFormFile file);
    }
}
