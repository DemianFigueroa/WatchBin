using WatchBin.Infrastructure.Entity;
using WatchBin.Domain.Models;

namespace WatchBin.Mappers
{
    public class MediaModelToEntityMapper : IMediaModelToEntityMapper
    {
        public MediaEntity Map(AddMediaRequestModel model)
        {
            return new MediaEntity
            {
                Id = Guid.NewGuid(),
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
    }
}