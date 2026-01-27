namespace Streaming.Application.DTOs.Episode;

public class EpisodeDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int EpisodeNumber { get; set; }
    public string? UrlVideo { get; set; }
    public int DurationMinutes { get; set; }
}