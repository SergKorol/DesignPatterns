using DataAccessPatterns.LazyLoadingProxyNaive.Models;

namespace DataAccessPatterns.LazyLoadingProxyNaive.Proxies;

public class LazyArtist : Artist
{
    private readonly LazyAlbumsLoader _lazyLoader;
    private List<Album>? _albums;
    private bool _isLoaded;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public LazyArtist(Artist artist, LazyAlbumsLoader lazyLoader)
    {
        ArtistId = artist.ArtistId;
        Name = artist.Name;
        _lazyLoader = lazyLoader;
        _isLoaded = false;
    }

    public new Task<List<Album>> Albums => GetAlbumsAsync();

    private async Task<List<Album>> GetAlbumsAsync()
    {
        if (_isLoaded) return _albums!;

        await _semaphore.WaitAsync();
        try
        {
            if (!_isLoaded)
            {
                _albums = await _lazyLoader(ArtistId);
                _isLoaded = true;
            }
        }
        finally
        {
            _semaphore.Release();
        }

        return _albums!;
    }
}
