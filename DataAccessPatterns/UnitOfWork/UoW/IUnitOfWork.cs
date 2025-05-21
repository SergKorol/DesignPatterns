using DataAccessPatterns.UnitOfWork.Models;
using DataAccessPatterns.UnitOfWork.Repositories;

namespace DataAccessPatterns.UnitOfWork.UoW;

public interface IUnitOfWork : IAsyncDisposable
{
    public IRepository<Artist> ArtistRepository { get; }
    public IRepository<Album> AlbumRepository { get; }
    
    Task<int> SaveChangesAsync();
}