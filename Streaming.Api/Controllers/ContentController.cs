using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
}