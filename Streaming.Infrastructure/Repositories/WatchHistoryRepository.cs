using Microsoft.EntityFrameworkCore;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;
using Streaming.Infrastructure.Data;

namespace Streaming.Infrastructure.Repositories;

public class WatchHistoryRepository : GenericRepository<WatchHistory>, IWatchHistoryRepository
{
    public WatchHistoryRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<WatchHistory>> GetByProfileIdAsync(Guid profileId)
    {
        return await _context.WatchHistories
            .Where(wh => wh.ProfileId == profileId)
            .OrderByDescending(wh => wh.LastWatchedAt)
            .Include(wh => wh.Content)
            .ToListAsync();
    }

    public async Task<WatchHistory?> GetLatestForContentAsync(Guid profileId, Guid contentId)
    {
        return await _context.WatchHistories
            .Where(wh => wh.ProfileId == profileId && wh.ContentId == contentId)
            .OrderByDescending(wh => wh.LastWatchedAt)
            .FirstOrDefaultAsync();
    }
}