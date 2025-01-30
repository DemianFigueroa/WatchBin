using WatchBin.Infrastructure.Entity;

namespace WatchBin.Domain.Repositories
{
    public interface IMediaRepository
    {
        Task<MediaEntity> AddAsync(MediaEntity entity, string userId);
        Task<MediaEntity?> GetByIdAsync(Guid id, string userId);
    }
}
