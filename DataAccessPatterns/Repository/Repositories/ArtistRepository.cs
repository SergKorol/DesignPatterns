using DataAccessPatterns.Repository.Context;
using DataAccessPatterns.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPatterns.Repository.Repositories;

public class ArtistRepository(MusicDbContext context) : Repository<Artist>(context)
{
    public override async Task<IEnumerable<Artist>> GetAllAsync()
    {
        return await context.Artists.Include(x => x.Albums).ToListAsync();
    }

    public override async Task<Artist?> GetByIdAsync(int id)
    {
        return await context.Artists.Include(x => x.Albums).FirstOrDefaultAsync(x => x.ArtistId == id);
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

