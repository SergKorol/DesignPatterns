using DataAccessPatterns.LazyLoadingProxyPackage.Context;
using DataAccessPatterns.LazyLoadingProxyPackage.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPatterns.LazyLoadingProxyPackage.Repositories;

public class ArtistRepository(MusicDbContext context) : EfRepository<Artist>(context)
{
    public override async Task<IEnumerable<Artist>> GetAllAsync()
    {
        return await context.Artists.ToListAsync();
    }
    
    public override async Task<Artist?> GetByIdAsync(int id)
    {
        return await context.Artists.FirstOrDefaultAsync(x => x.ArtistId == id);
    }

    public override async Task AddAsync(Artist artist)
    {
        await base.AddAsync(artist);
    }

    public override async Task UpdateAsync(Artist artist)
    {
        await base.UpdateAsync(artist);
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

