using Streaming.Application.DTOs.MyList;
using Streaming.Application.DTOs.WatchHistory;
using Streaming.Application.DTOs.Rating;
using Streaming.Application.Interfaces;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;

namespace Streaming.Application.Services;

public class UserInteractionService : IUserInteractionService
{
    private readonly IUnitOfWork _unitOfWork;

    public UserInteractionService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task SaveProgressAsync(Guid profileId, Guid contentId, int seconds)
    {
        // Buscamos si ya existe un registro de este contenido para este perfil
        var history = await _unitOfWork.WatchHistories.GetLatestForContentAsync(profileId, contentId);

        if (history == null)
        {
            history = new WatchHistory
            {
                Id = Guid.NewGuid(),
                ProfileId = profileId,
                ContentId = contentId,
                WatchedSeconds = seconds,
                LastWatchedAt = DateTime.UtcNow
            };
            await _unitOfWork.WatchHistories.AddAsync(history);
        }
        else
        {
            history.WatchedSeconds = seconds;
            history.LastWatchedAt = DateTime.UtcNow;
            _unitOfWork.WatchHistories.Update(history);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<WatchHistoryDto>> GetWatchHistoryAsync(Guid profileId)
    {
        var history = await _unitOfWork.WatchHistories.GetByProfileIdAsync(profileId);
    
        return history.Select(h => new WatchHistoryDto
        {
            ContentId = h.ContentId,
            ContentTitle = h.Content.Title,
            WatchedSeconds = h.WatchedSeconds,
            LastWatchedAt = h.LastWatchedAt
        });
    }

    public async Task ToggleMyListAsync(Guid profileId, Guid contentId)
    {
        // Buscamos si el contenido ya está en la lista de ese perfil
        var existingItem = (await _unitOfWork.MyLists.FindAsync(m => 
            m.ProfileId == profileId && m.ContentId == contentId)).FirstOrDefault();

        if (existingItem != null)
        {
            _unitOfWork.MyLists.Delete(existingItem);
        }
        else
        {
            var newItem = new MyList
            {
                ProfileId = profileId,
                ContentId = contentId,
                AddedAt = DateTime.UtcNow
            };
            await _unitOfWork.MyLists.AddAsync(newItem);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SetRatingAsync(Guid profileId, RatingRequest request)
    {
        var existingRating = (await _unitOfWork.Ratings.FindAsync(r => 
            r.ProfileId == profileId && r.ContentId == request.ContentId)).FirstOrDefault();

        if (existingRating != null)
        {
            existingRating.Value = request.Value;
            _unitOfWork.Ratings.Update(existingRating);
        }
        else
        {
            var newRating = new Rating
            {
                Id = Guid.NewGuid(),
                ProfileId = profileId,
                ContentId = request.ContentId,
                Value = request.Value
            };
            await _unitOfWork.Ratings.AddAsync(newRating);
        }

        await _unitOfWork.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<MyListDto>> GetMyListAsync(Guid profileId)
    {
        var items = await _unitOfWork.MyLists.FindAsync(m => m.ProfileId == profileId);
    
        return items.Select(m => new MyListDto {
            ContentId = m.ContentId,
            ContentTitle = m.Content?.Title ?? "Sin título",
            ThumbnailUrl = m.Content?.ThumbnailUrl,
            AddedAt = m.AddedAt
        });
    }
}