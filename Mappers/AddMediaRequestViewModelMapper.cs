using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public class AddMediaRequestViewModelMapper : IAddMediaRequestViewModelToModelMapper
    {
        public AddMediaRequestModel Map(AddMediaRequestViewModel model)
        {
            return new AddMediaRequestModel()
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
