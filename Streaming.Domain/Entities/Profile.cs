namespace Streaming.Domain.Entities;

public class Profile
{
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    
    public string Name { get; set; } = string.Empty;
    public bool IsKids { get; set; }
    public string? Lenguaje { get; set; }
    public string? AvatarUrl { get; set; }
    
    public ICollection<WatchHistory> WatchHistories { get; set; } = new List<WatchHistory>();
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}