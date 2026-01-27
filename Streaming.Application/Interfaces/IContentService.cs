using Streaming.Application.DTOs.Content;

namespace Streaming.Application.Interfaces;

public interface IContentService
{
    Task<IEnumerable<ContentSummaryDto>> GetCatalogAsync();
    Task<ContentDetailsDto?> GetDetailsAsync(Guid id);
    Task<IEnumerable<ContentSummaryDto>> GetByCategoryAsync(Guid genreId);
}