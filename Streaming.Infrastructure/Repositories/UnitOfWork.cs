using Streaming.Domain.Entities;
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
    public IGenericRepository<Profile> Profiles { get; private set; }
    public IGenericRepository<Plan> Plans { get; private set; }
    public IGenericRepository<MyList> MyLists { get; }
    public IGenericRepository<Rating> Ratings { get; }
    public IGenericRepository<Episode> Episodes { get; }
    public IGenericRepository<Season> Seasons { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;

        Users = new UserRepository(_context);
        Contents = new ContentRepository(_context);
        Subscriptions = new SubscriptionRepository(_context);
        WatchHistories = new WatchHistoryRepository(_context);
        Profiles = new GenericRepository<Profile>(_context);
        Plans = new GenericRepository<Plan>(_context);
        MyLists = new GenericRepository<MyList>(_context);
        Ratings = new GenericRepository<Rating>(_context);
        Episodes = new GenericRepository<Episode>(_context);
        Seasons = new GenericRepository<Season>(_context);
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