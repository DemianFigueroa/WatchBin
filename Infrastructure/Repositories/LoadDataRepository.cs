using WatchBin.Domain.Repositories;
using WatchBin.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace WatchBin.Infrastructure.Repositories
{
    public class LoadDataRepository : ILoadDataRepository
    {
        private readonly ApplicationDbContext _context;

        public LoadDataRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task LoadDataFromJson(string filePath)
        {
            string json = await File.ReadAllTextAsync(filePath);
            var extractedMedia = JsonConvert.DeserializeObject<List<MediaEntity>>(json);
            if (extractedMedia != null)
            {
                foreach (var media in extractedMedia)
                {
                    var existingMedia = await _context.Media.FirstOrDefaultAsync(b => b.Id == media.Id);

                    if (existingMedia != null)
                    {
                        existingMedia.Name = media.Name;
                        existingMedia.Creator = media.Creator;
                    }
                    else
                    {
                        _context.Media.Add(media);
                    }
                }
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"The backup is invalid");
            }
        }
    }
}