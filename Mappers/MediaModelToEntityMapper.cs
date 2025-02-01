using WatchBin.Domain.Models;
using WatchBin.Infrastructure.Entity;

namespace WatchBin.Mappers
{
    public class MediaModelToEntityMapper : IMediaModelToEntityMapper
    {
        public MediaEntity Map(AddMediaRequestModel model)
        {
            return new MediaEntity
            {
                Id = model.Id,
                UserId = model.UserId,
                Name = model.Name,
                Type = model.Type,
                Creator = model.Creator,
                ReleaseDate = model.ReleaseDate.ToUniversalTime(),
                Description = model.Description,
                CoverImage = model.CoverImage,
                Category = model.Category,
                Status = model.Status,
            };
        }
    }
}
