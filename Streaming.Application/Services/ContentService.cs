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
        var content = await _unitOfWork.Contents.GetContentDetailsAsync(id);
        if (content == null) return null;

        var details = new ContentDetailsDto
        {
            Id = content.Id,
            Title = content.Title,
            Description = content.Description,
            UrlVideo = content.UrlVideo,
            DurationMinutes = content.DurationMinutes,
            ContentType = content.ContentType.ToString(),
            Genres = content.ContentGenres.Select(cg => cg.Genre.Name).ToList(),
            Seasons = new List<SeasonDto>()
        };

        if (content.ContentType == Domain.Enums.ContentType.Series)
        {
            details.Seasons = content.Seasons.OrderBy(s => s.SeasonNumber).Select(s => new SeasonDto
            {
                Id = s.Id,
                SeasonNumber = s.SeasonNumber,
                Episodes = s.Episodes.OrderBy(e => e.EpisodeNumber).Select(e => new EpisodeDto
                {
                    Id = e.Id,
                    Title = e.Title,
                    EpisodeNumber = e.EpisodeNumber,
                    DurationMinutes = e.DurationMinutes,
                    UrlVideo = e.UrlVideo
                }).ToList()
            }).ToList();
        }

        return details;
    }

    public async Task<IEnumerable<ContentSummaryDto>> GetByCategoryAsync(Guid genreId)
    {
        var contents = await _unitOfWork.Contents.GetByGenreAsync(genreId);
    
        return contents.Select(c => new ContentSummaryDto
        {
            Id = c.Id,
            Title = c.Title,
            ThumbnailUrl = c.ThumbnailUrl,
            Description = c.Description
        });
    }
}