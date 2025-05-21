using DataAccessPatterns.Repository.Models;
using DataAccessPatterns.Repository.Repositories;

namespace DataAccessPatterns.Repository.Services;

public class AlbumService(IRepository<Album> albumRepository)
{
    public Task DeleteAlbumAsync(int id) => albumRepository.DeleteAsync(id);
}

