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
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Creator = model.Creator,
                ReleaseDate = model.ReleaseDate,
                Language = model.Language,
                Description = model.Description,
                CoverImage = model.CoverImage,
                Notes = model.Notes,
                Category = model.Category,
                AddedDate = model.AddedDate,
                Priority = model.Priority,
                Status = model.Status,
                CompletionStatus = model.CompletionStatus,
                Platform = model.Platform,
                Length = model.Length,
                IMBDCode = model.IMBDCode
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