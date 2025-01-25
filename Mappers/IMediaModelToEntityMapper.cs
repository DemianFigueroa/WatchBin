using WatchBin.Infrastructure.Entity;
using WatchBin.Domain.Models;

namespace WatchBin.Mappers
{
    public interface IMediaModelToEntityMapper
    {
        MediaEntity Map(AddMediaRequestModel model);
    }
}