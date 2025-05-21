namespace DataAccessPatterns.LazyLoadingProxyPackage.Models;

public class Album
{
    public int AlbumId { get; set; }
    public string? Title { get; set; }
    public int ArtistId { get; set; }
    public virtual Artist? Artist { get; set; }
}