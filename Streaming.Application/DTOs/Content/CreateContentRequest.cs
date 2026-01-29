using Microsoft.AspNetCore.Http;
using Streaming.Domain.Enums;

namespace Streaming.Application.DTOs.Content;

public class CreateContentRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public ContentType ContentType { get; set; }
    public string UrlVideo { get; set; } = string.Empty;

    public IFormFile? ImageFile { get; set; }
}