using Streaming.Domain.Interfaces;
using Streaming.Infrastructure.Data;

namespace Streaming.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    
    public IUserRepository Users { get; private set; }
    public IContentRepository Contents { get; private set; }
    public ISubscriptionRepository Subscriptions { get; private set; }
    public IWatchHistoryRepository WatchHistories { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Contents = new ContentRepository(_context);
        Subscriptions = new SubscriptionRepository(_context);
        WatchHistories = new WatchHistoryRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}