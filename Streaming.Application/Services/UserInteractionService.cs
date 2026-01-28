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
        var history = (await _unitOfWork.WatchHistories.FindAsync(h => 
            h.ProfileId == profileId && h.ContentId == contentId)).FirstOrDefault();

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
        // Aquí podrías necesitar un método específico en el repositorio para traer el Título del contenido
        var history = await _unitOfWork.WatchHistories.FindAsync(h => h.ProfileId == profileId);
        
        return history.Select(h => new WatchHistoryDto
        {
            ContentId = h.ContentId,
            WatchedSeconds = h.WatchedSeconds,
            LastWatchedAt = h.LastWatchedAt
            // ContentTitle se llenaría si haces un Include en el repositorio
        });
    }

    public async Task ToggleMyListAsync(Guid profileId, Guid contentId)
    {
        // Buscamos si el contenido ya está en la lista de ese perfil
        var existingItem = (await _unitOfWork.MyLists.FindAsync(m => 
            m.ProfileId == profileId && m.ContentId == contentId)).FirstOrDefault();

        if (existingItem != null)
        {
            // Si existe, lo borramos (Toggle Off)
            _unitOfWork.MyLists.Delete(existingItem);
        }
        else
        {
            // Si no existe, lo agregamos (Toggle On)
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
        // Buscamos si el perfil ya calificó este contenido anteriormente
        var existingRating = (await _unitOfWork.Ratings.FindAsync(r => 
            r.ProfileId == profileId && r.ContentId == request.ContentId)).FirstOrDefault();

        if (existingRating != null)
        {
            // Si ya existe, actualizamos el valor (Like/Dislike)
            existingRating.Value = request.Value;
            _unitOfWork.Ratings.Update(existingRating);
        }
        else
        {
            // Si es nuevo, creamos el registro
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
}