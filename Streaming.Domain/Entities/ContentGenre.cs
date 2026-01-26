namespace Streaming.Domain.Entities;

public class ContentGenre
{
    public Guid ContentId { get; set; }
    public Content Content { get; set; } = null!;
    
    public Guid GenreId { get; set; }
    public Genre Genre { get; set; } = null!;
}