using WatchBin.Domain.Models;
using WatchBin.Domain.Repositories;
using WatchBin.Domain.UseCases;
using WatchBin.Mappers;
using WatchBin.Domain.Respositories;

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
            IMediaEntityToModelMapper entityToModelMapper)
        {
            this.getMediaRepository = getMediaRepository;
            this.deleteRepository = deleteRepository;
            this.entityToModelMapper = entityToModelMapper;
        }

        public async Task<MediaModel> DeleteAsync(Guid MediaId)
        {
            var retrievedEntity = await getMediaRepository.GetAsync(MediaId);
            if (retrievedEntity == null)
            {
                throw new Exception("Media not found.");
            }
            await deleteRepository.DeleteAsync(MediaId);
            return entityToModelMapper.Map(retrievedEntity);
        }
    }
}