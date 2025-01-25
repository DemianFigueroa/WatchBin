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
            ApplicationDbContext context)
        {
            _context = context;
            this.addMediaUseCase = addMediaUseCase;
            this.getMediaUseCase = getMediaUseCase;
            this.addMediaRequestMapper = addMediaRequestMapper;
            this.getMediaRequestMapper = getMediaRequestMapper;
            this.modelMapper = modelMapper;


        }
        public async Task<MediaViewModel> AddAsync(AddMediaRequestViewModel request)
        {
            var req = addMediaRequestMapper.Map(request);
            var mediaModel = await addMediaUseCase.AddAsync(req);
            return modelMapper.Map(mediaModel);
        }
        public async Task<MediaViewModel> GetAllAsync(GetAllMediaRequestViewModel request)
        {
            var req = getMediaRequestMapper.Map(request);
            var mediaModel = await getMediaUseCase.GetAsync(req);
            return modelMapper.Map(mediaModel);
        }
        public async Task<GetMediaByIdRequestViewModel> GetByIdAsync(GetMediaByIdRequestViewModel request)
        {
            var model = await _context.Media.FindAsync(request.Id);
            if (model == null)
            {
                throw new KeyNotFoundException($"Media with ID {request.Id} was not found.");
            }
            return new GetMediaByIdRequestViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Creator = model.Creator,
                ReleaseDate = model.ReleaseDate,
                Language = model.Language,
                Description = model.Description,
                CoverImage = model.CoverImage,
                Notes = model.Notes,
                Category = model.Category,
                AddedDate = model.AddedDate,
                Priority = model.Priority,
                Status = model.Status,
                CompletionStatus = model.CompletionStatus,
                Platform = model.Platform,
                Length = model.Length,
                IMBDCode = model.IMBDCode
            };
        }
        public async Task<List<MediaViewModel>> GetAllAsync()
        {
            var media = await getMediaUseCase.GetAllAsync();
            var sortedMedia = media.OrderBy(model => model.Name);
            return sortedMedia.Select(model => new MediaViewModel
            {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                ReleaseDate = model.ReleaseDate,
                CoverImage = model.CoverImage,
                Category = model.Category,
                AddedDate = model.AddedDate,
                Priority = model.Priority,
                Status = model.Status,
                CompletionStatus = model.CompletionStatus,
                Length = model.Length,
            }).ToList();
        }
    }
}
