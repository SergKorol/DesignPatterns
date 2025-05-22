namespace DataAccessPatterns.LazyLoadingProxyPackage.Models;

public class Artist
{
    public int ArtistId { get; set; }
    public string? Name { get; set; }
    public virtual List<Album>? Albums { get; set; }
}