using WatchBin.Domain.Models;
using WatchBin.Infrastructure.Entity;

namespace WatchBin.Mappers
{
    public interface IMediaModelToEntityMapper
    {
        MediaEntity Map(AddMediaRequestModel model);
    }
}
