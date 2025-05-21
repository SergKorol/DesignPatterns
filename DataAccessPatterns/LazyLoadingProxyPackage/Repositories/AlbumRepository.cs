
using DataAccessPatterns.LazyLoadingProxyPackage.Context;
using DataAccessPatterns.LazyLoadingProxyPackage.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPatterns.LazyLoadingProxyPackage.Repositories;

public class AlbumRepository(MusicDbContext context)  : EfRepository<Album>(context)
{
    public override async Task<IEnumerable<Album>> GetAllAsync()
    {
        return await context.Albums.ToListAsync();
    }
    
    public override async Task<Album?> GetByIdAsync(int id)
    {
        return await context.Albums.FirstOrDefaultAsync(x => x.AlbumId == id);
    }

    public override async Task AddAsync(Album album)
    {
        await base.AddAsync(album);
    }

    public override async Task UpdateAsync(Album album)
    {
        await base.UpdateAsync(album);
    }

    public override async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            await base.DeleteAsync(id);
        }
    }
}