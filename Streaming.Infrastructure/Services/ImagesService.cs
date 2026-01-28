using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

using Streaming.Application.Interfaces;

namespace Streaming.Infrastructure.Services;

public class ImagesService : IImagesService 
{
    private readonly Cloudinary _cloudinary;

    public ImagesService(IConfiguration config) {
        var acc = new Account(
            config["Cloudinary:CloudName"],
            config["Cloudinary:ApiKey"],
            config["Cloudinary:ApiSecret"]
        );
        _cloudinary = new Cloudinary(acc);
    }

    public async Task<string> UploadPhotoAsync(IFormFile file) {
        if (file == null || file.Length == 0) return string.Empty;

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams {
            File = new FileDescription(file.FileName, stream),
            Transformation = new Transformation().Height(800).Width(600).Crop("fill").Gravity("auto") 
        };
        
        var uploadResult = await _cloudinary.UploadAsync(uploadParams);
        return uploadResult.SecureUrl.ToString();
    }
}