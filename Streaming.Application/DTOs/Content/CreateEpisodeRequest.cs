namespace Streaming.Application.DTOs.Content;

public class CreateEpisodeRequest
{
    public Guid SeasonId { get; set; }
    public string Title { get; set; } = string.Empty;
    public int EpisodeNumber { get; set; }
    public int DurationMinutes { get; set; }
    public string UrlVideo { get; set; } = string.Empty;
}