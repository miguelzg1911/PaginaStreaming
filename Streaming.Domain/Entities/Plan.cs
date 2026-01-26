namespace Streaming.Domain.Entities;

public class Plan
{
    public Guid Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public int MaxProfiles { get; set; }
    public string MaxResolution { get; set; } = String.Empty;
    public string? Description { get; set; } = String.Empty;
    
    public ICollection<Suscription> Suscriptions { get; set; } = new HashSet<Suscription>();
}