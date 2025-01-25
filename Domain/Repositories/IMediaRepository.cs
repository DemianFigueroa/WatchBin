using WatchBin.Infrastructure.Entity;

namespace WatchBin.Domain.Repositories
{
    public interface IMediaRepository
    {
        Task<MediaEntity> AddAsync(MediaEntity entity);
        Task<MediaEntity?> GetByIdAsync(Guid id);
    }
}