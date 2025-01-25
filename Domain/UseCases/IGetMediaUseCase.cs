using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Domain.UseCases
{
    public interface IGetMediaUseCase
    {
        Task<MediaModel> GetAsync(GetMediaRequestModel request);
        Task<List<MediaViewModel>> GetAllAsync();
    }
}
