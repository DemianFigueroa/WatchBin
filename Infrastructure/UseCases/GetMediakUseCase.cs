using WatchBin.Domain.Models;
using WatchBin.Mappers;
using WatchBin.Domain.Respositories;
using WatchBin.Domain.UseCases;
using WatchBin.ViewModels;

namespace WatchBin.Infrastructure.UseCases
{
    public class GetMediaUseCase : IGetMediaUseCase
    {
        private readonly IGetMediaRepository getMediaRepository;
        private readonly IMediaEntityToModelMapper entityToModelMapper;

        public GetMediaUseCase(
            IGetMediaRepository getMediaRepository,
            IMediaEntityToModelMapper entityToModelMapper)
        {
            this.getMediaRepository = getMediaRepository;
            this.entityToModelMapper = entityToModelMapper;
        }

        public async Task<List<MediaViewModel>> GetAllAsync()
        {
            var mediaEntities = await getMediaRepository.GetAllAsync();
            var mediaViewModels = mediaEntities.Select(entityToModelMapper.MapToViewModel).ToList();
            return mediaViewModels;
        }

        public async Task<MediaModel> GetAsync(GetMediaRequestModel request)
        {
            var retrievedEntity = await getMediaRepository.GetAsync(request.Id);
            if (retrievedEntity == null)
            {
                throw new Exception("Media not found.");
            }
            return entityToModelMapper.Map(retrievedEntity);
        }
    }
}