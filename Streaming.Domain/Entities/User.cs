using Streaming.Domain.Enums;

namespace Streaming.Domain.Entities;

public class User
{
    public Guid Id { get; set; } 
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    
    public ICollection<Profile> Profiles { get; set; } = new List<Profile>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}