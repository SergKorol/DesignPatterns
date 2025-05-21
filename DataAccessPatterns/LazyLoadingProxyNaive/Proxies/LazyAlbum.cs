using DataAccessPatterns.LazyLoadingProxyNaive.Models;

namespace DataAccessPatterns.LazyLoadingProxyNaive.Proxies;

public class LazyAlbum
{
    private readonly Album _album;
    private readonly LazyArtistLoader _lazyLoader;

    private Artist? _artist;

    public LazyAlbum(Album album, LazyArtistLoader lazyLoader)
    {
        _album = album;
        _lazyLoader = lazyLoader;
    }

    public int AlbumId => _album.AlbumId;
    public string? Title => _album.Title;
    public int ArtistId => _album.ArtistId;

    public async Task<Artist?> Artist()
    {
        if (_artist == null)
        {
            _artist = await _lazyLoader(_album.AlbumId);
        }

        return _artist;
    }
}
