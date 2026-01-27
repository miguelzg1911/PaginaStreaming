namespace Streaming.Application.DTOs.Auth;

public class AuthResponse
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; }
    public string Username { get; set; } = string.Empty;
    public Guid UserId { get; set; }
}