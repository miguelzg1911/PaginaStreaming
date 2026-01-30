using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streaming.Application.DTOs.Content;
using Streaming.Application.Interfaces;

namespace Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContentController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentController(IContentService contentService)
    {
        _contentService = contentService;
    }

    [HttpGet("catalog")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCatalog()
    {
        var content = await _contentService.GetCatalogAsync();
        return Ok(content);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDetails(Guid id)
    {
        var details = await _contentService.GetDetailsAsync(id);
        if (details == null) return NotFound();
        return Ok(details);
    }
    
    [HttpGet("genre/{genreId}")]
    public async Task<IActionResult> GetByGenre(Guid genreId)
    {
        var content = await _contentService.GetByCategoryAsync(genreId);
        return Ok(content);
    }
    
    [HttpGet("filter/{type}")]
    public async Task<IActionResult> GetByType(string type)
    {
        var contents = await _contentService.GetByTypeAsync(type);
        return Ok(contents);
    }
}