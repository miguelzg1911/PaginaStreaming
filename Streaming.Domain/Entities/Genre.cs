namespace Streaming.Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    
    public ICollection<Content> Contents { get; set; } = new List<Content>();
    public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
}