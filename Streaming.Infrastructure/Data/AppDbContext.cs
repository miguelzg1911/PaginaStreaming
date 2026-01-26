using Microsoft.EntityFrameworkCore;
using Streaming.Domain.Entities;

namespace Streaming.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Content> Contents { get; set; }
    public DbSet<ContentGenre> ContentGenres { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<MyList> MyLists { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<Profile> Profiles { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<WatchHistory> WatchHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}