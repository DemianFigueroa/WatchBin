using WatchBin.Infrastructure.Entity;

namespace WatchBin.Domain.Respositories
{
    public interface IGetMediaRepository
    {
        Task<MediaEntity?> GetAsync(Guid id, string userId);
        Task<List<MediaEntity>> GetAllAsync(string userId);
        Task<MediaEntity> DeleteAsync(Guid id, string userId);
    }
}
