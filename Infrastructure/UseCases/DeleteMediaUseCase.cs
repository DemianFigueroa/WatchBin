using WatchBin.Domain.Models;
using WatchBin.Domain.Repositories;
using WatchBin.Domain.Respositories;
using WatchBin.Domain.UseCases;
using WatchBin.Mappers;

namespace WatchBin.Infrastructure.UseCases
{
    public class DeleteMediaUseCase : IDeleteMediaUseCase
    {
        private readonly IGetMediaRepository getMediaRepository;
        private readonly IDeleteMediaRepository deleteRepository;
        private readonly IMediaEntityToModelMapper entityToModelMapper;

        public DeleteMediaUseCase(
            IGetMediaRepository getMediaRepository,
            IDeleteMediaRepository deleteRepository,
            IMediaEntityToModelMapper entityToModelMapper
        )
        {
            this.getMediaRepository = getMediaRepository;
            this.deleteRepository = deleteRepository;
            this.entityToModelMapper = entityToModelMapper;
        }

        public async Task<MediaModel> DeleteAsync(Guid MediaId, string userId)
        {
            var retrievedEntity = await getMediaRepository.GetAsync(MediaId, userId);
            if (retrievedEntity == null)
            {
                throw new Exception("Media not found or you do not have permission to delete it.");
            }
            await deleteRepository.DeleteAsync(MediaId, userId);
            return entityToModelMapper.Map(retrievedEntity);
        }
    }
}
