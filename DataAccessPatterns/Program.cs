using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DataAccessPatterns.LazyLoadingProxyNaive;
using DataAccessPatterns.Repository.Models;
using DataAccessPatterns.Repository.Repositories;
using Settings;


namespace DataAccessPatterns;

public class Program
{
    public static async Task Main(string[] args)
    {
        BenchmarkRunner.Run<Program>(new BenchmarkConfig());
    }

    [Benchmark]
    public async Task RunRepository()
    {
        await using var dbContext = new Repository.Context.MusicDbContext();
        var artistRepository = new ArtistRepository(dbContext);
        var albumRepository = new AlbumRepository(dbContext);
        var artistService = new Repository.Services.ArtistService(artistRepository);
        var albumService = new Repository.Services.AlbumService(albumRepository);

        Console.WriteLine("All artists:");
        var artists = await artistService.GetAllArtistsAsync();
        foreach (var artist in artists)
        {
            var albums = artist.Albums.Any() ? artist.Albums.Select(x => x.Title).ToList()! : ["No albums"];
            Console.WriteLine($"{artist.ArtistId}: {artist.Name}. Discography: { string.Join(", ", albums) }");
        }

        // Add new artist
        var newArtist = new Artist { Name = "EF Core Artist", Albums = new List<Album> { new Album { Title = "EF Core Album" } } };
        await artistService.AddArtistAsync(newArtist);
        Console.WriteLine($"Added artist with ID {newArtist.ArtistId}");

        // Update artist
        newArtist.Name = "Updated EF Core Artist";
        await artistService.UpdateArtistAsync(newArtist);
        Console.WriteLine("Updated artist.");
        
        // Get by ID
        var artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", artistById.Albums.Select(x => x.Title).ToList())})");
        
        //Delete album
        await albumService.DeleteAlbumAsync(artistById.Albums.First().AlbumId);
        artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        if (artistById != null && !artistById.Albums.Any())
        {
            Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", ["No albums"])})");
        }

        // Delete artist
        await artistService.DeleteArtistAsync(newArtist.ArtistId);
        Console.WriteLine("Deleted artist.");

        Console.WriteLine("Done.");
    }
    
    [Benchmark]
    public async Task RunUnitOfWork()
    {
        await using var dbContext = new UnitOfWork.Context.MusicDbContext();
        var uow = new UnitOfWork.UoW.UnitOfWork(dbContext);
        var artistService = new UnitOfWork.Services.ArtistService(uow);
        var albumService = new UnitOfWork.Services.AlbumService(uow);

        Console.WriteLine("All artists:");
        var artists = await artistService.GetAllArtistsAsync();
        foreach (var artist in artists)
        {
            var albums = artist.Albums.Any() ? artist.Albums.Select(x => x.Title).ToList()! : ["No albums"];
            Console.WriteLine($"{artist.ArtistId}: {artist.Name}. Discography: { string.Join(", ", albums) }");
        }

        // Add new artist
        var newArtist = new UnitOfWork.Models.Artist { Name = "EF Core Artist", Albums = new List<UnitOfWork.Models.Album> { new UnitOfWork.Models.Album { Title = "EF Core Album" } } };
        await artistService.AddArtistAsync(newArtist);
        Console.WriteLine($"Added artist with ID {newArtist.ArtistId}");

        // Update artist
        newArtist.Name = "Updated EF Core Artist";
        await artistService.UpdateArtistAsync(newArtist);
        Console.WriteLine("Updated artist.");
        
        // Get by ID
        var artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", artistById.Albums.Select(x => x.Title).ToList())})");
        
        //Delete album
        await albumService.DeleteAlbumAsync(artistById.Albums.First().AlbumId);
        artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        if (artistById != null && !artistById.Albums.Any())
        {
            Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", ["No albums"])})");
        }

        // Delete artist
        await artistService.DeleteArtistAsync(newArtist.ArtistId);
        Console.WriteLine("Deleted artist.");

        Console.WriteLine("Done.");
    }
    
    [Benchmark]
    public async Task RunLazyLoadingProxyNaive()
    {
        await using var dbContext = new LazyLoadingProxyNaive.Context.MusicDbContext();
        var artistRepository = new LazyLoadingProxyNaive.Repositories.ArtistRepository(dbContext);
        var albumRepository = new LazyLoadingProxyNaive.Repositories.AlbumRepository(dbContext);
        var lazyFactory = new LazyFactory(artistRepository, albumRepository);
        var artistService = new LazyLoadingProxyNaive.Services.ArtistService(artistRepository, lazyFactory);
        var albumService = new LazyLoadingProxyNaive.Services.AlbumService(albumRepository);

        Console.WriteLine("All artists:");
        var artists = await artistService.GetAllArtistsAsync();
        foreach (var artist in artists)
        {
            var albums = (await artist.Albums).Any() ? (await artist.Albums).Select(x => x.Title).ToList()! : ["No albums"];
            Console.WriteLine($"{artist.ArtistId}: {artist.Name}. Discography: { string.Join(", ", albums) }");
        }

        // Add new artist
        var newArtist = new LazyLoadingProxyNaive.Models.Artist { Name = "EF Core Artist", Albums = new List<LazyLoadingProxyNaive.Models.Album> { new LazyLoadingProxyNaive.Models.Album { Title = "EF Core Album" } } };
        await artistService.AddArtistAsync(newArtist);
        Console.WriteLine($"Added artist with ID {newArtist.ArtistId}");

        // Update artist
        newArtist.Name = "Updated EF Core Artist";
        await artistService.UpdateArtistAsync(newArtist);
        Console.WriteLine("Updated artist.");
        
        // Get by ID
        var artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", (await artistById.Albums).Select(x => x.Title).ToList())})");
        
        //Delete album
        await albumService.DeleteAlbumAsync((await artistById.Albums).First().AlbumId);
        artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        if (artistById != null && !(await artistById.Albums).Any())
        {
            Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", ["No albums"])})");
        }

        // Delete artist
        await artistService.DeleteArtistAsync(newArtist.ArtistId);
        Console.WriteLine("Deleted artist.");

        Console.WriteLine("Done.");
    }
    
    [Benchmark]
    public async Task RunLazyLoadingProxyPackage()
    {
        await using var dbContext = new LazyLoadingProxyPackage.Context.MusicDbContext();
        var artistRepository = new LazyLoadingProxyPackage.Repositories.ArtistRepository(dbContext);
        var albumRepository = new LazyLoadingProxyPackage.Repositories.AlbumRepository(dbContext);
        var artistService = new LazyLoadingProxyPackage.Services.ArtistService(artistRepository);
        var albumService = new LazyLoadingProxyPackage.Services.AlbumService(albumRepository);

        Console.WriteLine("All artists:");
        var artists = await artistService.GetAllArtistsAsync();
        foreach (var artist in artists)
        {
            var albums = artist.Albums.Any() ? artist.Albums.Select(x => x.Title).ToList()! : ["No albums"];
            Console.WriteLine($"{artist.ArtistId}: {artist.Name}. Discography: { string.Join(", ", albums) }");
        }

        // Add new artist
        var newArtist = new LazyLoadingProxyPackage.Models.Artist { Name = "EF Core Artist", Albums = new List<LazyLoadingProxyPackage.Models.Album> { new LazyLoadingProxyPackage.Models.Album { Title = "EF Core Album" } } };
        await artistService.AddArtistAsync(newArtist);
        Console.WriteLine($"Added artist with ID {newArtist.ArtistId}");

        // Update artist
        newArtist.Name = "Updated EF Core Artist";
        await artistService.UpdateArtistAsync(newArtist);
        Console.WriteLine("Updated artist.");
        
        // Get by ID
        var artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", artistById.Albums.Select(x => x.Title).ToList())})");
        
        //Delete album
        await albumService.DeleteAlbumAsync(artistById.Albums.First().AlbumId);
        artistById = await artistService.GetArtistByIdAsync(newArtist.ArtistId);
        if (artistById != null && !artistById.Albums.Any())
        {
            Console.WriteLine($"Artist by ID: {artistById?.ArtistId} - {artistById?.Name} ({string.Join(", ", ["No albums"])})");
        }

        // Delete artist
        await artistService.DeleteArtistAsync(newArtist.ArtistId);
        Console.WriteLine("Deleted artist.");

        Console.WriteLine("Done.");
    }
}