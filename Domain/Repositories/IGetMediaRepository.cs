using WatchBin.Infrastructure.Entity;

namespace WatchBin.Domain.Respositories
{
    public interface IGetMediaRepository
    {
        Task<MediaEntity?> GetAsync(Guid id);
        Task<List<MediaEntity>> GetAllAsync();
        Task<MediaEntity> DeleteAsync(Guid id);
    }

}
