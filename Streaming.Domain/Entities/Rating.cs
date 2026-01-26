namespace Streaming.Domain.Entities;

public class Rating
{
    public Guid Id { get; set; }
    
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;
    
    public Guid ContentId { get; set; }
    public Content Content { get; set; } = null!;
    
    public string Value { get; set; } = String.Empty;
}