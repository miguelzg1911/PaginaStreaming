using Streaming.Application.DTOs.Profile;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

namespace Streaming.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProfileService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<IEnumerable<ProfileDto>> GetProfilesByUserIdAsync(Guid userId)
    {
        var profiles = await _unitOfWork.Profiles.FindAsync(p => p.UserId == userId);
        return profiles.Select(p => new ProfileDto {
            Id = p.Id, 
            Name = p.Name, 
            IsKids = p.IsKids, 
            AvatarUrl = p.AvatarUrl
        });
    }

    public async Task<ProfileDto> CreateProfileAsync(Guid userId, CreateProfileRequest request)
    {
        var subscription = await _unitOfWork.Subscriptions.GetActiveSubscriptionAsync(userId);
    
        if (subscription == null) 
        {
            throw new Exception("No tienes una suscripción activa para crear perfiles.");
        }

        var maxProfiles = subscription.Plan.MaxProfiles;

        var currentProfiles = await _unitOfWork.Profiles.FindAsync(p => p.UserId == userId);

        if (currentProfiles.Count() >= maxProfiles)
        {
            throw new Exception($"Tu plan {subscription.Plan.Name} solo permite {maxProfiles} perfiles.");
        }

        var profile = new Profile {
            Id = Guid.NewGuid(),
            UserId = userId,
            Name = request.Name,
            IsKids = request.IsKids,
            Language = request.Language ?? "es",
            AvatarUrl = "default-avatar.png"
        };

        await _unitOfWork.Profiles.AddAsync(profile);
        await _unitOfWork.SaveChangesAsync();

        return new ProfileDto { Id = profile.Id, Name = profile.Name, IsKids = profile.IsKids, AvatarUrl = profile.AvatarUrl };
    }

    public async Task UpdateProfileAsync(Guid profileId, UpdateProfileRequest request)
    {
        var profile = await _unitOfWork.Profiles.GetByIdAsync(profileId);
        if (profile == null) throw new Exception("Perfil no encontrado");

        profile.Name = request.Name;
        profile.IsKids = request.IsKids;
        profile.Language = request.Language ?? profile.Language;

        _unitOfWork.Profiles.Update(profile);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProfileAsync(Guid profileId)
    {
        var profile = await _unitOfWork.Profiles.GetByIdAsync(profileId);
        if (profile == null) throw new Exception("Perfil no encontrado");

        _unitOfWork.Profiles.Delete(profile);
        await _unitOfWork.SaveChangesAsync();
    }
}