using WatchBin.Infrastructure.Entity;

namespace WatchBin.Domain.Repositories
{
    public interface IDeleteMediaRepository
    {
        Task<MediaEntity> DeleteAsync(Guid id, string userId);
    }
}
