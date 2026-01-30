using Microsoft.EntityFrameworkCore;
using Streaming.Domain.Entities;
using Streaming.Domain.Interfaces;
using Streaming.Infrastructure.Data;

namespace Streaming.Infrastructure.Repositories;

public class MyListRepository : GenericRepository<MyList>, IMyListRepository
{
    public MyListRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<MyList>> GetByProfileIdWithContentAsync(Guid profileId)
    {
        return await _context.MyLists
            .Include(m => m.Content)
            .Where(m => m.ProfileId == profileId)
            .ToListAsync();
    }
}