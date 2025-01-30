using Microsoft.EntityFrameworkCore;
using WatchBin.Domain.Respositories;
using WatchBin.Infrastructure.Entity;

namespace WatchBin.Infrastructure.Repositories
{
    public class AddMediaRepository : IAddMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public AddMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MediaEntity> AddAsync(MediaEntity entity, string userId)
        {
            var existingEntity = await _context.Media.FirstOrDefaultAsync(m =>
                m.Id == entity.Id && m.UserId == userId
            );

            if (existingEntity != null)
            {
                // Update existing entity
                _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                // Add new entity
                entity.UserId = userId;
                _context.Media.Add(entity);
            }

            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
