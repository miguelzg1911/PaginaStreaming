namespace Streaming.Application.DTOs.WatchHistory;

public class WatchHistoryDto
{
    public Guid ContentId { get; set; }
    public string ContentTitle { get; set; } = string.Empty;
    public int WatchedSeconds { get; set; }
    public DateTime LastWatchedAt { get; set; }
}