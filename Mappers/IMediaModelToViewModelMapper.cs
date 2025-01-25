using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public interface IMediaModelToViewModelMapper
    {
        MediaViewModel Map(MediaModel source);
    }
}
