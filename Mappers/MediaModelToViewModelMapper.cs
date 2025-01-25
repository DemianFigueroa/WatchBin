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
                Name = model.Name,
                Type = model.Type,
                ReleaseDate = model.ReleaseDate,
                CoverImage = model.CoverImage,
                Category = model.Category,
                AddedDate = model.AddedDate,
                Priority = model.Priority,
                Status = model.Status,
                CompletionStatus = model.CompletionStatus,
                Length = model.Length,
            };
        }
    }
}
