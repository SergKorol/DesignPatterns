using DataAccessPatterns.UnitOfWork.Context;
using DataAccessPatterns.UnitOfWork.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPatterns.UnitOfWork.Repositories;

public class AlbumRepository(MusicDbContext context)  : Repository<Album>(context)
{
    public override async Task<IEnumerable<Album>> GetAllAsync()
    {
        return await context.Albums.Include(x => x.Artist).ToListAsync();
    }
    
    public override async Task<Album?> GetByIdAsync(int id)
    {
        return await context.Albums.Include(x => x.Artist).FirstOrDefaultAsync(x => x.AlbumId == id);
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