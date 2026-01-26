namespace Streaming.Domain.Entities;

public class Season
{
    public Guid Id { get; set; }
    
    public Guid ContentId { get; set; }
    public Content Content { get; set; } = null!;
    
    public int SeasonNumber { get; set; }
    
    public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
}