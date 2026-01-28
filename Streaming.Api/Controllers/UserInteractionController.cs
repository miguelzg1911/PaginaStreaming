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

    [HttpPost("mylist/toggle")]
    public async Task<IActionResult> ToggleMyList(Guid profileId, Guid contentId)
    {
        await _interactionService.ToggleMyListAsync(profileId, contentId);
        return Ok();
    }

    [HttpPost("rating")]
    public async Task<IActionResult> SetRating(Guid profileId, [FromBody] RatingRequest request)
    {
        await _interactionService.SetRatingAsync(profileId, request);
        return Ok();
    }
}