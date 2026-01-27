using Streaming.Application.DTOs.Content;
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
        var content = await _unitOfWork.Contents.GetByIdAsync(id);
        if (content == null) return null;

        return new ContentDetailsDto
        {
            Id = content.Id,
            Title = content.Title,
            Description = content.Description,
            UrlVideo = content.UrlVideo,
            DurationMinutes = content.DurationMinutes,
            ContentType = content.ContentType.ToString(),
            Genres = new List<string> { "Accion", "Drama" },
            Seasons = new List<SeasonDto>()
        };
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