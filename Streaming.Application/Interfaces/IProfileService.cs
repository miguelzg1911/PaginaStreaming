using Streaming.Application.DTOs.Profile;

namespace Streaming.Application.Interfaces;

public interface IProfileService
{
    Task<IEnumerable<ProfileDto>> GetProfilesByUserIdAsync(Guid userId);
    Task<ProfileDto> CreateProfileAsync(Guid userId, CreateProfileRequest request);
}
