using Streaming.Application.DTOs.Plan;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

namespace Streaming.Application.Services;

public class SubscriptionService : ISubscriptionService
{
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
    {
        var plans = await _unitOfWork.Plans.GetAllAsync();
        return plans.Select(p => new PlanDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            MaxResolution = p.MaxResolution
        });
    }

    public async Task<bool> IsUserSubscribedAsync(Guid userId)
    {
        var subscriptions = await _unitOfWork.Subscriptions.FindAsync(s => 
            s.UserId == userId && s.EndDate > DateTime.UtcNow && s.IsActive);
        
        return subscriptions.Any();
    }

    public async Task<bool> SubscribeUserAsync(Guid userId, Guid planId)
    {
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PlanId = planId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            IsActive = true
        };

        await _unitOfWork.Subscriptions.AddAsync(subscription);
        return await _unitOfWork.SaveChangesAsync() > 0;
    }
}