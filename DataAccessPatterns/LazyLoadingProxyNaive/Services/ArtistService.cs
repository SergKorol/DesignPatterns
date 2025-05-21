using DataAccessPatterns.LazyLoadingProxyNaive.Models;
using DataAccessPatterns.LazyLoadingProxyNaive.Proxies;
using DataAccessPatterns.LazyLoadingProxyNaive.Repositories;

namespace DataAccessPatterns.LazyLoadingProxyNaive.Services;

public class ArtistService(IRepository<Artist> artistRepository, ILazyFactory lazyFactory)
{
    public Task<List<LazyArtist>> GetAllArtistsAsync() => lazyFactory.GetAllLazyArtistsAsync();

    public Task<LazyArtist?> GetArtistByIdAsync(int id) => lazyFactory.GetLazyArtistByIdAsync(id);

    public Task AddArtistAsync(Artist artist) => artistRepository.AddAsync(artist);

    public Task UpdateArtistAsync(Artist artist) => artistRepository.UpdateAsync(artist);

    public Task DeleteArtistAsync(int id) => artistRepository.DeleteAsync(id);
}
