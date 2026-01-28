using Microsoft.AspNetCore.Http;

namespace Streaming.Application.Interfaces;

public interface IImagesService
{
    Task<string> UploadPhotoAsync(IFormFile file);
}