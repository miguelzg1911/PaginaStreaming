using Microsoft.AspNetCore.Http;

namespace Practica.Application.Interfaces;

public interface IImagesService
{
    Task<string> UploadPhotoAsync(IFormFile file);
}