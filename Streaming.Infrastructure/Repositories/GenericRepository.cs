using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Streaming.Domain.Interfaces;
using Streaming.Infrastructure.Data;

namespace Streaming.Infrastructure.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
        
    public GenericRepository(AppDbContext context) => _context = context;

    public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id);
    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>()
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);

    public void Update(T entity) => _context.Set<T>().Update(entity);

    public void Delete(T entity) => _context.Set<T>().Remove(entity);

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}