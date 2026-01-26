using Streaming.Domain.Entities;

namespace Streaming.Domain.Interfaces;

public interface IWatchHistoryRepository : IGenericRepository<WatchHistory>
{
    Task<IEnumerable<WatchHistory>> GetByProfileIdAsync(Guid profileId);
    Task<WatchHistory?> GetLatestForContentAsync(Guid profileId, Guid contentId);
}