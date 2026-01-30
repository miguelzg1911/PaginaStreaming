using Streaming.Domain.Entities;

namespace Streaming.Domain.Interfaces;

public interface IContentRepository : IGenericRepository<Content>
{
    Task<Content?> GetContentDetailsAsync(Guid id);
    Task<IEnumerable<Content>> GetByTypeAsync(string type);
    Task<IEnumerable<Content>> GetByGenreAsync(Guid genreId);
}