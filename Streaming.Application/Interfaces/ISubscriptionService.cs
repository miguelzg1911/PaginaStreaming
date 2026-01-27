using Streaming.Application.DTOs.Plan;

namespace Streaming.Application.Interfaces;

public interface ISubscriptionService
{
    Task<IEnumerable<PlanDto>> GetAllPlansAsync();

    Task<bool> IsUserSubscribedAsync(Guid userId);
    
    Task<bool> SubscribeUserAsync(Guid userId, Guid planId);
}