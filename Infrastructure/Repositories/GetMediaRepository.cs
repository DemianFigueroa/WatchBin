using WatchBin.Infrastructure.Entity;
using WatchBin.Domain.Respositories;
using Microsoft.EntityFrameworkCore;

namespace WatchBin.Infrastructure.Repositories
{
    public class GetMediaRepository : IGetMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public GetMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MediaEntity?> GetAsync(Guid id)
        {
            var entity = await _context.Media.FindAsync(id);
            if (entity == null)
            {
                throw new Exception("Media entity not found.");
            }
            return entity;
        }
        public async Task<List<MediaEntity>> GetAllAsync()
        {
            return await _context.Media.ToListAsync();
        }
        public async Task<MediaEntity> DeleteAsync(Guid id)
        {
            var media = await _context.Media.FindAsync(id);
            if (media != null)
            {
                _context.Media.Remove(media);
                await _context.SaveChangesAsync();
            }
            throw new InvalidOperationException($"Media with ID {id} does not exist.");
        }
    }
}