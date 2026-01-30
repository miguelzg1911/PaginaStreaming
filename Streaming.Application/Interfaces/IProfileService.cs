using Streaming.Application.DTOs.Profile;

namespace Streaming.Application.Interfaces;

public interface IProfileService
{
    Task<IEnumerable<ProfileDto>> GetProfilesByUserIdAsync(Guid userId);
    Task<ProfileDto> CreateProfileAsync(Guid userId, CreateProfileRequest request);
    Task UpdateProfileAsync(Guid profileId, Guid userId, UpdateProfileRequest request);
    Task DeleteProfileAsync(Guid profileId, Guid userId);
}
