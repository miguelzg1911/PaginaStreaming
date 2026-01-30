using Streaming.Domain.Entities;

namespace Streaming.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    // Repositorios Especializados
    IUserRepository Users { get; }
    IContentRepository Contents { get; }
    ISubscriptionRepository Subscriptions { get; }
    IWatchHistoryRepository WatchHistories { get; }
    IMyListRepository MyLists { get; }

    // Repositorios Genéricos
    IGenericRepository<Profile> Profiles { get; }
    IGenericRepository<Plan> Plans { get; }
    IGenericRepository<Rating> Ratings { get; }
    IGenericRepository<Episode> Episodes { get; }
    IGenericRepository<Season> Seasons { get; }
    IGenericRepository<Genre> Genres { get; }
    IGenericRepository<ContentGenre> ContentGenres { get; }

    Task<int> SaveChangesAsync();
}