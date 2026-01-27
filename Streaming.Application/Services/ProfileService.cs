using Streaming.Application.DTOs.Profile;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

namespace Streaming.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfileService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<ProfileDto>> GetProfilesByUserIdAsync(Guid userId)
    {
        var profiles = await _unitOfWork.Profiles.FindAsync(p => p.UserId == userId);

        return profiles.Select(p => new ProfileDto
        {
            Id = p.Id,
            Name = p.Name,
            IdKids = p.IsKids,
            AvatarUrl = p.AvatarUrl
        });
    }

    public async Task<ProfileDto> CreateProfileAsync(Guid userId, CreateProfileRequest request)
    {
        var profile = new Profile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = request.Name,
            IsKids = request.IsKids,
            Language = request.Language ?? "es",
            AvatarUrl = "default-avatar.png"
        };
        
        await _unitOfWork.Profiles.AddAsync(profile);
        await _unitOfWork.SaveChangesAsync();

        return new ProfileDto
        {
            Id = profile.Id,
            Name = profile.Name,
            IsKids = profile.IsKids,
            AvatarUrl = profile.AvatarUrl
        };
    }
}