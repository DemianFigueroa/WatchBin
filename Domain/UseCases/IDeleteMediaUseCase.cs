using WatchBin.Domain.Models;

namespace WatchBin.Domain.UseCases
{
    public interface IDeleteMediaUseCase
    {
        Task<MediaModel> DeleteAsync(Guid id, string userId);
    }
}
