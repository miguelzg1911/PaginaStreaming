using Streaming.Application.DTOs.Rating;
using Streaming.Application.DTOs.WatchHistory;

namespace Streaming.Application.Interfaces;

public interface IUserInteractionService
{
    // Historial de visualización
    Task SaveProgressAsync(Guid profileId, Guid contentId, int seconds);
    Task<IEnumerable<WatchHistoryDto>> GetWatchHistoryAsync(Guid profileId);
    
    // Interacciones
    Task ToggleMyListAsync(Guid profileId, Guid contentId);
    Task SetRatingAsync(Guid profileId, RatingRequest request);
}