using Microsoft.EntityFrameworkCore;
using WatchBin.Domain.UseCases;
using WatchBin.Infrastructure.Repositories;
using WatchBin.Mappers;
using WatchBin.ViewModels;

namespace WatchBin.Services
{
    public class MediaService : IMediaService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAddMediaUseCase addMediaUseCase;
        private readonly IGetMediaUseCase getMediaUseCase;
        private readonly IAddMediaRequestViewModelToModelMapper addMediaRequestMapper;
        private readonly IGetMediaRequestViewModelToModelMapper getMediaRequestMapper;
        private readonly IMediaModelToViewModelMapper modelMapper;

        public MediaService(
            IAddMediaUseCase addMediaUseCase,
            IGetMediaUseCase getMediaUseCase,
            IAddMediaRequestViewModelToModelMapper addMediaRequestMapper,
            IGetMediaRequestViewModelToModelMapper getMediaRequestMapper,
            IMediaModelToViewModelMapper modelMapper,
            ApplicationDbContext context
        )
        {
            _context = context;
            this.addMediaUseCase = addMediaUseCase;
            this.getMediaUseCase = getMediaUseCase;
            this.addMediaRequestMapper = addMediaRequestMapper;
            this.getMediaRequestMapper = getMediaRequestMapper;
            this.modelMapper = modelMapper;
        }

        public async Task<MediaViewModel> AddAsync(AddMediaRequestViewModel request, string userId)
        {
            var req = addMediaRequestMapper.Map(request);
            var mediaModel = await addMediaUseCase.AddAsync(req, userId);
            return modelMapper.Map(mediaModel);
        }

        public async Task<MediaViewModel> GetAllAsync(
            GetAllMediaRequestViewModel request,
            string userId
        )
        {
            var req = getMediaRequestMapper.Map(request);
            var mediaModel = await getMediaUseCase.GetAsync(req, userId);
            return modelMapper.Map(mediaModel);
        }

        public async Task<GetMediaByIdRequestViewModel> GetByIdAsync(
            GetMediaByIdRequestViewModel request
        )
        {
            var model = await _context
                .Media.Where(m => m.Id == request.Id && m.UserId == request.UserId) // Ensure UserId matches
                .FirstOrDefaultAsync();

            if (model == null)
            {
                throw new KeyNotFoundException($"Media with ID {request.Id} was not found.");
            }

            return new GetMediaByIdRequestViewModel
            {
                UserId = model.UserId,
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Creator = model.Creator,
                ReleaseDate = model.ReleaseDate,
                Description = model.Description,
                CoverImage = model.CoverImage,
                Category = model.Category,
                Status = model.Status,
            };
        }

        public async Task<List<MediaViewModel>> GetAllAsync(string userId)
        {
            var media = await _context
                .Media.Where(m => m.UserId == userId) // Filter by UserId
                .OrderBy(m => m.Name)
                .ToListAsync();

            return media
                .Select(model => new MediaViewModel
                {
                    UserId = model.UserId,
                    Id = model.Id,
                    Name = model.Name,
                    Type = model.Type,
                    ReleaseDate = model.ReleaseDate,
                    CoverImage = model.CoverImage,
                    Category = model.Category,
                    Status = model.Status,
                })
                .ToList();
        }
    }
}
