using Microsoft.EntityFrameworkCore;
using WatchBin.Domain.Repositories;
using WatchBin.Infrastructure.Entity;

namespace WatchBin.Infrastructure.Repositories
{
    public class DeleteMediaRepository : IDeleteMediaRepository
    {
        private readonly ApplicationDbContext _context;

        public DeleteMediaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<MediaEntity> DeleteAsync(Guid id, string userId)
        {
            var media = await _context.Media.FirstOrDefaultAsync(m =>
                m.Id == id && m.UserId == userId
            );
            if (media != null)
            {
                _context.Media.Remove(media);
                await _context.SaveChangesAsync();
                return media;
            }
            throw new InvalidOperationException(
                $"Media with ID {id} does not exist or doesn't belong to the user."
            );
        }
    }
}
