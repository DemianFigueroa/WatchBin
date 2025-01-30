using WatchBin.Domain.Models;
using WatchBin.ViewModels;

namespace WatchBin.Mappers
{
    public interface IAddMediaRequestViewModelToModelMapper
    {
        AddMediaRequestModel Map(AddMediaRequestViewModel source);
    }
}
