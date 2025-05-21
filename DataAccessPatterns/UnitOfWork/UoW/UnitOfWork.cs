using DataAccessPatterns.UnitOfWork.Context;
using DataAccessPatterns.UnitOfWork.Models;
using DataAccessPatterns.UnitOfWork.Repositories;

namespace DataAccessPatterns.UnitOfWork.UoW;

public class UnitOfWork : IUnitOfWork
{
    private readonly MusicDbContext _context;
    public IRepository<Artist> ArtistRepository { get; }
    
    public IRepository<Album> AlbumRepository { get; }

    public UnitOfWork(MusicDbContext context)
    {
        _context = context;
        ArtistRepository = new ArtistRepository(_context);
        AlbumRepository = new AlbumRepository(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}