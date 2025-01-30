using WatchBin.ViewModels;

namespace WatchBin.Services
{
    public interface IMediaService
    {
        Task<MediaViewModel> AddAsync(AddMediaRequestViewModel request, string userId);
        Task<MediaViewModel> GetAllAsync(GetAllMediaRequestViewModel request, string userId);
        Task<GetMediaByIdRequestViewModel> GetByIdAsync(GetMediaByIdRequestViewModel request);
        Task<List<MediaViewModel>> GetAllAsync(string userId);
    }
}
