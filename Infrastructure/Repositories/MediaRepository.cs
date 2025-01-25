using WatchBin.Domain.Repositories;
using WatchBin.Infrastructure.Entity;

namespace WatchBin.Infrastructure.Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public MediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MediaEntity> AddAsync(MediaEntity entity)
        {
            _context.Media.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MediaEntity?> GetByIdAsync(Guid id)
        {
            return await _context.Media.FindAsync(id);
        }
    }
}