using DataAccessPatterns.LazyLoadingProxyNaive.Models;

namespace DataAccessPatterns.LazyLoadingProxyNaive;

public delegate Task<List<Album>> LazyAlbumsLoader(int artistId);
public delegate Task<Artist?> LazyArtistLoader(int albumId);