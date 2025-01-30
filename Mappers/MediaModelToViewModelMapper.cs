using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public class MediaModelToViewModelMapper : IMediaModelToViewModelMapper
    {
        public MediaViewModel Map(MediaModel model)
        {
            return new MediaViewModel()
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                Type = model.Type,
                ReleaseDate = model.ReleaseDate,
                CoverImage = model.CoverImage,
                Category = model.Category,
                Status = model.Status,
            };
        }
    }
}
