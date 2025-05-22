
using DataAccessPatterns.LazyLoadingProxyPackage.Context;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPatterns.LazyLoadingProxyPackage.Repositories;

public abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly MusicDbContext _context;
    private readonly DbSet<T> _dbSet;

    protected Repository(MusicDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}