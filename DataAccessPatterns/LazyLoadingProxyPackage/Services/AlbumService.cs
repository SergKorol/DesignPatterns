using DataAccessPatterns.LazyLoadingProxyPackage.Models;
using DataAccessPatterns.LazyLoadingProxyPackage.Repositories;

namespace DataAccessPatterns.LazyLoadingProxyPackage.Services;

public class AlbumService(IRepository<Album> albumRepository)
{
    public Task DeleteAlbumAsync(int id) => albumRepository.DeleteAsync(id);
}

