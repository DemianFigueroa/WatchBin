using System.Text;
using WatchBin.Domain.Repositories;
using WatchBin.Domain.UseCases;
using WatchBin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchBin.Services;

namespace WatchBin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IDeleteMediaUseCase _deleteMediaUseCase;
        private readonly ILoadDataRepository _loadDataRepository;
        public MediaController(IMediaService mediaService, IDeleteMediaUseCase deleteMediaUseCase, ILoadDataRepository loadDataRepository)
        {
            _mediaService = mediaService;
            _deleteMediaUseCase = deleteMediaUseCase;
            _loadDataRepository = loadDataRepository;
        }
        [HttpGet("getAllMedia")]
        public async Task<IActionResult> GetAllMedia()
        {
            var media = await _mediaService.GetAllAsync();
            return Ok(media);
        }
        [HttpGet("getMediaByid")]
        public async Task<IActionResult> GetMedia(Guid id)
        {
            var request = new GetMediaByIdRequestViewModel { Id = id };
            var media = await _mediaService.GetByIdAsync(request);

            if (media == null)
            {
                return NotFound();
            }

            return Ok(media);
        }
        [HttpPost("addMedia")]
        public async Task<IActionResult> AddMedia([FromBody] AddMediaRequestViewModel request)
        {
            var media = await _mediaService.AddAsync(request);
            return Ok(media);
        }
        [HttpDelete("deleteById")]
        public async Task<IActionResult> DeleteMedia(Guid id)
        {
            try
            {
                var deletedMedia = await _deleteMediaUseCase.DeleteAsync(id);
                return Ok(deletedMedia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("createBackup")]
        public async Task<IActionResult> DownloadBackup()
        {
            try
            {
                var media = await _mediaService.GetAllAsync();
                var json = JsonConvert.SerializeObject(media, Formatting.Indented);
                var bytes = Encoding.UTF8.GetBytes(json);
                var fileName = $"backup_{DateTime.Now.ToString("yyyyMMddHHmmss")}.json";
                return File(bytes, "application/json", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpPost("loadBackup")]
        public async Task<IActionResult> LoadDataFromJson([FromBody] string filePath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(filePath))
                {
                    return BadRequest("File path is required.");
                }
                if (!System.IO.File.Exists(filePath))
                {
                    return NotFound("File not found.");
                }
                await _loadDataRepository.LoadDataFromJson(filePath);
                return Ok("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}