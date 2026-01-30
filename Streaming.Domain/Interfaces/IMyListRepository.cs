using Streaming.Domain.Entities;

namespace Streaming.Domain.Interfaces;

public interface IMyListRepository : IGenericRepository<MyList>
{
    Task<IEnumerable<MyList>> GetByProfileIdWithContentAsync(Guid profileId);
}