using WatchBin.Domain.Models;
using WatchBin.Domain.Respositories;
using WatchBin.Domain.UseCases;
using WatchBin.Mappers;

namespace WatchBin.Infrastructure.UseCases
{
    public class AddMediaUseCase : IAddMediaUseCase
    {
        private readonly IAddMediaRepository addRepository;
        private readonly IGetMediaRepository getMediaRepository;
        private readonly IMediaModelToEntityMapper modelToEntityMapper;
        private readonly IMediaEntityToModelMapper entityToModelMapper;

        public AddMediaUseCase(
            IAddMediaRepository addRepository,
            IGetMediaRepository getMediaRepository,
            IMediaModelToEntityMapper modelToEntityMapper,
            IMediaEntityToModelMapper entityToModelMapper
        )
        {
            this.addRepository = addRepository;
            this.getMediaRepository = getMediaRepository;
            this.modelToEntityMapper = modelToEntityMapper;
            this.entityToModelMapper = entityToModelMapper;
        }

        public async Task<MediaModel> AddAsync(AddMediaRequestModel request, string userId)
        {
            var mediaEntity = modelToEntityMapper.Map(request);
            mediaEntity.UserId = userId;
            var addedEntity = await addRepository.AddAsync(mediaEntity, userId);
            var retrievedEntity = await getMediaRepository.GetAsync(addedEntity.Id, userId);
            if (retrievedEntity == null)
            {
                throw new Exception("Failed to retrieve the added media entity.");
            }
            return entityToModelMapper.Map(retrievedEntity);
        }
    }
}
