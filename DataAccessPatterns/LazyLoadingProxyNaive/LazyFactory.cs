using DataAccessPatterns.LazyLoadingProxyNaive.Proxies;
using DataAccessPatterns.LazyLoadingProxyNaive.Repositories;

namespace DataAccessPatterns.LazyLoadingProxyNaive;

public class LazyFactory(ArtistRepository artistRepo, AlbumRepository albumRepo) : ILazyFactory
{
    public async Task<List<LazyArtist>> GetAllLazyArtistsAsync()
    {
        var artists = await artistRepo.GetAllAsync();
        var lazyArtists = artists.Select(a => new LazyArtist(a, artistId => albumRepo.GetByForeignKeyAsync(artistId)))
            .ToList();
        return lazyArtists;
    }

    public async Task<LazyArtist?> GetLazyArtistByIdAsync(int id)
    {
        var artist = await artistRepo.GetByIdAsync(id);
        if (artist == null) return null;

        var lazyArtist = new LazyArtist(artist, async artistId =>
            (await albumRepo.GetByForeignKeyAsync(artistId)).ToList());
        return lazyArtist;
    }
}

public interface ILazyFactory
{
    Task<List<LazyArtist>> GetAllLazyArtistsAsync();
    Task<LazyArtist?> GetLazyArtistByIdAsync(int id);
}