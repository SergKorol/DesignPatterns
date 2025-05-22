namespace DataAccessPatterns.Repository.Models;

public class Artist
{
    public int ArtistId { get; set; }
    public string? Name { get; set; }

    public List<Album>? Albums { get; set; }
}