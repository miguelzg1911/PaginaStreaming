namespace Streaming.Application.DTOs.Content;

public class AssignGenresRequest 
{
    public Guid ContentId { get; set; }
    public List<Guid> GenreIds { get; set; } = new();
}