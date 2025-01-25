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
    }

}
