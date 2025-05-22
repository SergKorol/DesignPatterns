using DataAccessPatterns.LazyLoadingProxyNaive.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessPatterns.LazyLoadingProxyNaive.Context;

public class MusicDbContext : DbContext
{
    public DbSet<Artist> Artists { get; set; }
    public DbSet<Album> Albums { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=chinook.db");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artist>(entity =>
        {
            entity.ToTable("artists");
            entity.HasKey(e => e.ArtistId);
            entity.Property(e => e.ArtistId).HasColumnName("ArtistId");
            entity.Property(e => e.Name).HasColumnName("Name").HasMaxLength(120);

            entity.HasMany(a => a.Albums)
                .WithOne(b => b.Artist)
                .HasForeignKey(b => b.ArtistId);
        });

        modelBuilder.Entity<Album>(entity =>
        {
            entity.ToTable("albums");
            entity.HasKey(e => e.AlbumId);
            entity.Property(e => e.AlbumId).HasColumnName("AlbumId");
            entity.Property(e => e.Title).HasColumnName("Title").HasMaxLength(160);
            entity.Property(e => e.ArtistId).HasColumnName("ArtistId");
        });
    }
}
