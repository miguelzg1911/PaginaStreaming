using Streaming.Application.DTOs.Content;
using Streaming.Application.DTOs.Episode;
using Streaming.Application.DTOs.Season;
using Streaming.Application.Interfaces;
using Streaming.Domain.Interfaces;

namespace Streaming.Application.Services;

public class ContentService : IContentService
{
    private readonly IUnitOfWork _unitOfWork;

    public ContentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<ContentSummaryDto>> GetCatalogAsync()
    {
        var contentList = await _unitOfWork.Contents.GetAllAsync();

        return contentList.Select(c => new ContentSummaryDto
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            ThumbnailUrl = c.ThumbnailUrl
        });
    }

    public async Task<ContentDetailsDto?> GetDetailsAsync(Guid id)
    {
        // Buscamos el contenido base
        var content = await _unitOfWork.Contents.GetByIdAsync(id);
        if (content == null) return null;

        // Mapeamos los datos básicos
        var details = new ContentDetailsDto
        {
            Id = content.Id,
            Title = content.Title,
            Description = content.Description,
            UrlVideo = content.UrlVideo, // Si es película, este es el link principal
            DurationMinutes = content.DurationMinutes,
            ContentType = content.ContentType.ToString(),
            Seasons = new List<SeasonDto>(),
            
            Genres = content.ContentGenres.Select(cg => cg.Genre.Name).ToList(),
        };

        // Si es una serie, cargamos la estructura completa
        if (content.ContentType == Domain.Enums.ContentType.Series)
        {
            // Buscamos las temporadas que pertenezcan a este contenido
            var seasons = await _unitOfWork.Seasons.FindAsync(s => s.ContentId == id);
        
            foreach (var season in seasons.OrderBy(s => s.SeasonNumber))
            {
                // Buscamos los episodios de cada temporada
                var episodes = await _unitOfWork.Episodes.FindAsync(e => e.SeasonId == season.Id);
            
                details.Seasons.Add(new SeasonDto
                {
                    Id = season.Id,
                    SeasonNumber = season.SeasonNumber,
                    Episodes = episodes.OrderBy(e => e.EpisodeNumber).Select(e => new EpisodeDto
                    {
                        Id = e.Id,
                        Title = e.Title,
                        EpisodeNumber = e.EpisodeNumber,
                        DurationMinutes = e.DurationMinutes,
                        UrlVideo = e.UrlVideo
                    }).ToList()
                });
            }
        }

        return details;
    }

    public async Task<IEnumerable<ContentSummaryDto>> GetByCategoryAsync(Guid genreId)
    {
        var contents = await _unitOfWork.Contents.GetByTypeAsync(genreId.ToString());
        return contents.Select(c => new ContentSummaryDto
        {
            Id = c.Id,
            Title = c.Title,
            ThumbnailUrl = c.ThumbnailUrl
        });
    }
}