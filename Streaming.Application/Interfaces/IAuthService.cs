using Streaming.Application.DTOs.Auth;

namespace Streaming.Application.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> LoginAsync(LoginRequest request);
    
    Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken);
}