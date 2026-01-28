using System.Security.Claims;
using Streaming.Application.DTOs.Auth;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Enums;
using Streaming.Domain.Interfaces;

namespace Streaming.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
    }
    
    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existingUser = await _unitOfWork.Users.GetByEmailAsync(request.Email);
        if (existingUser != null) throw new Exception("El correo ya esta registrado");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            CreatedAt = DateTime.UtcNow,
            IsActive = true,
            Role = UserRole.User
        };
        
        await _unitOfWork.Users.AddAsync(user);
        
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            PlanId = request.PlanId,
            StartDate = DateTime.UtcNow,
            EndDate = DateTime.UtcNow.AddMonths(1),
            IsActive = true
        };
        await _unitOfWork.Subscriptions.AddAsync(subscription);
        
        await _unitOfWork.SaveChangesAsync();
        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(request.Email);

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new Exception("Credenciales Invalidas");

        return await GenerateAuthResponse(user);
    }

    public async Task<AuthResponse> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        var userId = Guid.Parse(principal.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        var user = await _unitOfWork.Users.GetByIdAsync(userId);

        if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            throw new Exception("Token de refresco invalido o expirado");
        
        return await GenerateAuthResponse(user);
    }
    
    private async Task<AuthResponse> GenerateAuthResponse(User user)
    {
        var token = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveChangesAsync();

        return new AuthResponse
        {
            Token = token,
            RefreshToken = refreshToken,
            Username = user.Username,
            UserId = user.Id
        };
    }
}