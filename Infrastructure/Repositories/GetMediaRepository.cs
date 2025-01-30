using Microsoft.EntityFrameworkCore;
using WatchBin.Domain.Repositories;
using WatchBin.Domain.Respositories;
using WatchBin.Infrastructure.Entity;

namespace WatchBin.Infrastructure.Repositories
{
    public class GetMediaRepository : IGetMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public GetMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MediaEntity?> GetAsync(Guid id, string userId)
        {
            var entity = await _context.Media.FirstOrDefaultAsync(media =>
                media.Id == id && media.UserId == userId
            );

            if (entity == null)
            {
                throw new Exception("Media entity not found or does not belong to the user.");
            }

            return entity;
        }

        public async Task<List<MediaEntity>> GetAllAsync(string userId)
        {
            return await _context.Media.Where(media => media.UserId == userId).ToListAsync();
        }

        public async Task<MediaEntity> DeleteAsync(Guid id, string userId)
        {
            var media = await _context.Media.FirstOrDefaultAsync(media =>
                media.Id == id && media.UserId == userId
            );

            if (media != null)
            {
                _context.Media.Remove(media);
                await _context.SaveChangesAsync();
                return media;
            }

            throw new InvalidOperationException(
                $"Media with ID {id} does not exist or does not belong to the user."
            );
        }
    }
}
