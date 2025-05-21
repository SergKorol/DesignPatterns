using DataAccessPatterns.UnitOfWork.UoW;

namespace DataAccessPatterns.UnitOfWork.Services;

public class AlbumService(IUnitOfWork uow)
{
    public Task DeleteAlbumAsync(int id) => uow.AlbumRepository.DeleteAsync(id);
}