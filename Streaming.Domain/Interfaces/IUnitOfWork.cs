namespace Streaming.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IContentRepository Contents { get; }
    ISubscriptionRepository Subscriptions { get; }
    IWatchHistoryRepository WatchHistories { get; }
    
    Task<int> SaveChangesAsync();
}