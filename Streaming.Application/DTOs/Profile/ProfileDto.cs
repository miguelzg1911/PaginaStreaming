namespace Streaming.Application.DTOs.Profile;

public class ProfileDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsKids { get; set; }
    public string? AvatarUrl { get; set; }
}