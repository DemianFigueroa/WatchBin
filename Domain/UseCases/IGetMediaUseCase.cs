using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Domain.UseCases
{
    public interface IGetMediaUseCase
    {
        Task<MediaModel> GetAsync(GetMediaRequestModel request, string userId);
        Task<List<MediaViewModel>> GetAllAsync(string userId);
    }
}
