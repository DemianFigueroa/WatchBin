using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public interface IGetMediaRequestViewModelToModelMapper
    {
        GetMediaRequestModel Map(GetAllMediaRequestViewModel source);
    }

    public class GetMediaRequestViewModelToModelMapper : IGetMediaRequestViewModelToModelMapper
    {
        public GetMediaRequestModel Map(GetAllMediaRequestViewModel model)
        {
            return new GetMediaRequestModel()
            {
                UserId = model.UserId,
                Id = model.Id,
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
