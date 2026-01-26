using Microsoft.EntityFrameworkCore;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;
using Streaming.Infrastructure.Data;

namespace Streaming.Infrastructure.Repositories;

public class ContentRepository : GenericRepository<Content>, IContentRepository
{
    public ContentRepository(AppDbContext context) : base(context) { }

    public async Task<Content?> GetContentDetailsAsync(Guid id)
    {
        return await _context.Contents
            .Include(c => c.ContentGenres)
            .ThenInclude(cg => cg.Genre)
            .Include(c => c.Seasons)
            .ThenInclude(s => s.Episodes)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Content>> GetByTypeAsync(string type)
    {
        return await _context.Contents
            .Where(c => c.ContentType.ToString() == type)
            .ToListAsync();
    }
}