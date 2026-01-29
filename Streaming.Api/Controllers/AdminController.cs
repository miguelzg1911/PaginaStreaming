using Microsoft.AspNetCore.Mvc;
using Streaming.Application.DTOs.Content;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

namespace Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IImagesService _imagesService;
    private readonly IUnitOfWork _unitOfWork;

    public AdminController(IImagesService imagesService, IUnitOfWork unitOfWork)
    {
        _imagesService = imagesService;
        _unitOfWork = unitOfWork;
    }

    [HttpPost("upload-content")]
    public async Task<IActionResult> UploadContent([FromForm] CreateContentRequest request)
    {
        string imageUrl = string.Empty;
        if (request.ImageFile != null)
        {
            imageUrl = await _imagesService.UploadPhotoAsync(request.ImageFile);
        }

        var newContent = new Content
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            ReleaseYear = request.ReleaseYear,
            ContentType = request.ContentType,
            UrlVideo = request.UrlVideo,
            ThumbnailUrl = imageUrl
        };

        await _unitOfWork.Contents.AddAsync(newContent);
        await _unitOfWork.SaveChangesAsync();
        
        return Ok(new { message = "Contenido subido con exito", contentId = newContent.Id });
    }
    
}