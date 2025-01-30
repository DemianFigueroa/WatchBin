using WatchBin.Domain.Models;
using WatchBin.Infrastructure.Entity;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public class MediaEntityToModelMapper : IMediaEntityToModelMapper
    {
        public MediaModel Map(MediaEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            return new MediaModel
            {
                UserId = model.UserId,
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Creator = model.Creator,
                ReleaseDate = model.ReleaseDate,
                Description = model.Description,
                CoverImage = model.CoverImage,
                Category = model.Category,
                Status = model.Status,
            };
        }

        public MediaViewModel MapToViewModel(MediaEntity model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            return new MediaViewModel
            {
                UserId = model.UserId,
                Id = model.Id,
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
