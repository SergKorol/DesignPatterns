using DataAccessPatterns.UnitOfWork.Models;
using DataAccessPatterns.UnitOfWork.UoW;

namespace DataAccessPatterns.UnitOfWork.Services;

public class ArtistService(IUnitOfWork uow)
{
    public Task<IEnumerable<Artist>> GetAllArtistsAsync() => uow.ArtistRepository.GetAllAsync();

    public Task<Artist?> GetArtistByIdAsync(int id) => uow.ArtistRepository.GetByIdAsync(id);

    public Task AddArtistAsync(Artist artist) => uow.ArtistRepository.AddAsync(artist);

    public Task UpdateArtistAsync(Artist artist) => uow.ArtistRepository.UpdateAsync(artist);

    public Task DeleteArtistAsync(int id) => uow.ArtistRepository.DeleteAsync(id);
}
