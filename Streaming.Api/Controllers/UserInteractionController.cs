using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streaming.Application.DTOs.Rating;
using Streaming.Application.Interfaces;

namespace Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserInteractionController : ControllerBase
{
    private readonly IUserInteractionService _interactionService;

    public UserInteractionController(IUserInteractionService interactionService)
    {
        _interactionService = interactionService;
    }

    [HttpPost("progress")]
    public async Task<IActionResult> SaveProgress(Guid profileId, Guid contentId, int seconds)
    {
        await _interactionService.SaveProgressAsync(profileId, contentId, seconds);
        return Ok();
    }

    [HttpPost("mylist/toggle/{profileId}/{contentId}")]
    public async Task<IActionResult> ToggleMyList(Guid profileId, Guid contentId)
    {
        await _interactionService.ToggleMyListAsync(profileId, contentId);
        return Ok(new { message = "Lista actualizada" });
    }

    [HttpPost("rating")]
    public async Task<IActionResult> SetRating(Guid profileId, [FromBody] RatingRequest request)
    {
        await _interactionService.SetRatingAsync(profileId, request);
        return Ok();
    }
    
    [HttpGet("history/{profileId}")]
    public async Task<IActionResult> GetHistory(Guid profileId)
    {
        var history = await _interactionService.GetWatchHistoryAsync(profileId);
        return Ok(history);
    }

    [HttpGet("mylist/{profileId}")]
    public async Task<IActionResult> GetMyList(Guid profileId)
    {
        var list = await _interactionService.GetMyListAsync(profileId);
        return Ok(list);
    }
}