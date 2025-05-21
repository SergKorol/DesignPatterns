using DataAccessPatterns.LazyLoadingProxyPackage.Models;
using DataAccessPatterns.LazyLoadingProxyPackage.Repositories;

namespace DataAccessPatterns.LazyLoadingProxyPackage.Services;

public class ArtistService(IRepository<Artist> artistRepository)
{
    public Task<IEnumerable<Artist>> GetAllArtistsAsync() => artistRepository.GetAllAsync();

    public Task<Artist?> GetArtistByIdAsync(int id) => artistRepository.GetByIdAsync(id);

    public Task AddArtistAsync(Artist artist) => artistRepository.AddAsync(artist);

    public Task UpdateArtistAsync(Artist artist) => artistRepository.UpdateAsync(artist);

    public Task DeleteArtistAsync(int id) => artistRepository.DeleteAsync(id);
}
