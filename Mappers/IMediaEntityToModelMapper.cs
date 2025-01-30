using WatchBin.Domain.Models;
using WatchBin.Infrastructure.Entity;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public interface IMediaEntityToModelMapper
    {
        MediaModel Map(MediaEntity entity);
        MediaViewModel MapToViewModel(MediaEntity entity);
    }
}
