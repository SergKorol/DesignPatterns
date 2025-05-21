using DataAccessPatterns.LazyLoadingProxyNaive.Models;
using DataAccessPatterns.LazyLoadingProxyNaive.Repositories;

namespace DataAccessPatterns.LazyLoadingProxyNaive.Services;

public class AlbumService(IRepository<Album> albumRepository)
{
    public Task DeleteAlbumAsync(int id) => albumRepository.DeleteAsync(id);
}

