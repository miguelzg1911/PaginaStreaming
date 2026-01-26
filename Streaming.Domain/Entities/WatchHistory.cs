namespace Streaming.Domain.Entities;

public class WatchHistory
{
    public Guid Id { get; set; }
    
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
    
    public Guid ContentId { get; set; }
    public Content Content { get; set; } = null!;
    
    public Guid? EpisodeId { get; set; }
    public Episode? Episode { get; set; }

    public int WatchedSeconds { get; set; }
    public bool Completed { get; set; }
    public DateTime LastWatchedAt { get; set; }
}