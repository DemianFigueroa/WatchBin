using WatchBin.Domain.Models;

namespace WatchBin.Domain.UseCases
{
    public interface IAddMediaUseCase
    {
        Task<MediaModel> AddAsync(AddMediaRequestModel request, string userId);
    }
}
