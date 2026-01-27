using Streaming.Domain.Entities;

namespace Streaming.Domain.Interfaces;

public interface ISubscriptionRepository : IGenericRepository<Subscription>
{
    Task<Subscription?> GetActiveSubscriptionAsync(Guid userId);
}