using WatchBin.ViewModels;

namespace WatchBin.Services
{
    public interface IMediaService
    {
        Task<MediaViewModel> AddAsync(AddMediaRequestViewModel request);
        Task<MediaViewModel> GetAllAsync(GetAllMediaRequestViewModel request);
        Task<GetMediaByIdRequestViewModel> GetByIdAsync(GetMediaByIdRequestViewModel request);
        Task<List<MediaViewModel>> GetAllAsync();
    }
}
