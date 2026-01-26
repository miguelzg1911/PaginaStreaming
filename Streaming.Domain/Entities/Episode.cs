namespace Streaming.Domain.Entities;

public class Episode
{
    public Guid Id { get; set; }
    
    public Guid SeasonId { get; set; }
    public Season Season { get; set; } = null!;
    
    public string Title { get; set; } = String.Empty;
    public int EpisodeNumber { get; set; }
    public int DurationMinutes { get; set; }
}