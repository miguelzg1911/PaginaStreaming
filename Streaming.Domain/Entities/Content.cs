using Streaming.Domain.Enums;

namespace Streaming.Domain.Entities;

public class Content
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int ReleaseYear { get; set; }
    public int DurationMinutes { get; set; }
    public ContentType ContentType { get; set; }
    public AgeRating AgeRating { get; set; }
    
    public string? ThumbnailUrl { get; set; }
    public string? UrlVideo { get; set; }
    
    public ICollection<Season> Seasons { get; set; } = new List<Season>();
    public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
}