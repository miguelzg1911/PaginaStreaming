using Streaming.Domain.Entities;

namespace Streaming.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IUserRepository Users { get; }
    IContentRepository Contents { get; }
    ISubscriptionRepository Subscriptions { get; }
    IWatchHistoryRepository WatchHistories { get; }
    IGenericRepository<Profile> Profiles { get; }
    IGenericRepository<Plan> Plans { get; }
    IGenericRepository<MyList> MyLists { get; }
    IGenericRepository<Rating> Ratings { get; }
    IGenericRepository<Episode> Episodes { get; }
    IGenericRepository<Season> Seasons { get; }

    Task<int> SaveChangesAsync();
}