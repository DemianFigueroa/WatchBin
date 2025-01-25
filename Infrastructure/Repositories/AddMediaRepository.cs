using WatchBin.Infrastructure.Entity;
using WatchBin.Domain.Respositories;

namespace WatchBin.Infrastructure.Repositories
{
    public class AddMediaRepository : IAddMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public AddMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MediaEntity> AddAsync(MediaEntity entity)
        {
            _context.Media.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}