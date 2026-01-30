using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Streaming.Application.Interfaces;

namespace Streaming.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubscriptionController : ControllerBase
{
    private readonly ISubscriptionService _subscriptionService;

    public SubscriptionController(ISubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

    [HttpGet("plans")]
    public async Task<IActionResult> GetAllPlans()
    {
        var plans = await _subscriptionService.GetAllPlansAsync();
        return Ok(plans);
    }

    [Authorize]
    [HttpPost("subscribe/{planId}")]
    public async Task<IActionResult> Subscribe(Guid planId)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var success = await _subscriptionService.SubscribeUserAsync(userId, planId);
        
        if (!success) return BadRequest(new { message = "No se pudo procesar la suscripción" });
        return Ok(new { message = "Suscripción exitosa" });
    }
    
    [Authorize]
    [HttpGet("my-status")]
    public async Task<IActionResult> GetMyStatus()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var isSubscribed = await _subscriptionService.IsUserSubscribedAsync(userId);
        return Ok(new { isSubscribed });
    }
}