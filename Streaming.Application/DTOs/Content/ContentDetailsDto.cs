using Streaming.Application.DTOs.Season;

namespace Streaming.Application.DTOs.Content;

public class ContentDetailsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? UrlVideo { get; set; }
    public int DurationMinutes { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public List<string> Genres { get; set; } = new List<string>();
    public List<SeasonDto>? Seasons { get; set; }
}