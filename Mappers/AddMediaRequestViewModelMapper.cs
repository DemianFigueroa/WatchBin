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
