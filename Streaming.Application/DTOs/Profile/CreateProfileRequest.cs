namespace Streaming.Application.DTOs.Profile;

public class CreateProfileRequest
{
    public string Name { get; set; } = string.Empty;
    public bool IsKids { get; set; }
    public string? Language { get; set; }
}