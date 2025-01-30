using Microsoft.EntityFrameworkCore;
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

        public async Task<MediaEntity> AddAsync(MediaEntity entity, string userId)
        {
            _context.Media.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MediaEntity?> GetByIdAsync(Guid id, string userId)
        {
            return await _context.Media.FirstOrDefaultAsync(media =>
                media.Id == id && media.UserId == userId
            );
        }
    }
}
