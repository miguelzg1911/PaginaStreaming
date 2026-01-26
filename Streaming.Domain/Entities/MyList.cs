namespace Streaming.Domain.Entities;

public class MyList
{
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!; 

    public Guid ContentId { get; set; }
    public Content Content { get; set; } = null!;
    
    public DateTime AddedAt { get; set; }
}