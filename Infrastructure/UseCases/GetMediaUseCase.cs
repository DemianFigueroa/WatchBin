using WatchBin.Domain.Models;
using WatchBin.Domain.Respositories;
using WatchBin.Domain.UseCases;
using WatchBin.Mappers;
using WatchBin.ViewModels;

namespace WatchBin.Infrastructure.UseCases
{
    public class GetMediaUseCase : IGetMediaUseCase
    {
        private readonly IGetMediaRepository getMediaRepository;
        private readonly IMediaEntityToModelMapper entityToModelMapper;

        public GetMediaUseCase(
            IGetMediaRepository getMediaRepository,
            IMediaEntityToModelMapper entityToModelMapper
        )
        {
            this.getMediaRepository = getMediaRepository;
            this.entityToModelMapper = entityToModelMapper;
        }

        public async Task<List<MediaViewModel>> GetAllAsync(string userId)
        {
            var mediaEntities = await getMediaRepository.GetAllAsync(userId);
            var mediaViewModels = mediaEntities.Select(entityToModelMapper.MapToViewModel).ToList();
            return mediaViewModels;
        }

        public async Task<MediaModel> GetAsync(GetMediaRequestModel request, string userId)
        {
            var retrievedEntity = await getMediaRepository.GetAsync(request.Id, userId);
            if (retrievedEntity == null)
            {
                throw new Exception("Media not found or you do not have permission to access it.");
            }
            return entityToModelMapper.Map(retrievedEntity);
        }
    }
}
