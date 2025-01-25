using WatchBin.Infrastructure.Entity;

namespace WatchBin.Domain.Respositories
{
    public interface IAddMediaRepository
    {
        Task<MediaEntity> AddAsync(MediaEntity entity);
    }

}
