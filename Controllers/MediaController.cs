using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WatchBin.Domain.UseCases;
using WatchBin.Services;
using WatchBin.Users;
using WatchBin.ViewModels;

namespace WatchBin.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MediaController : ControllerBase
    {
        private readonly IMediaService _mediaService;
        private readonly IDeleteMediaUseCase _deleteMediaUseCase;
        private readonly UserManager<AppUser> _userManager;

        public MediaController(
            IMediaService mediaService,
            IDeleteMediaUseCase deleteMediaUseCase,
            UserManager<AppUser> userManager
        )
        {
            _mediaService = mediaService;
            _deleteMediaUseCase = deleteMediaUseCase;
            _userManager = userManager;
        }

        private string? GetUserId()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        [HttpGet("getAllMedia")]
        public async Task<IActionResult> GetAllMedia()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("id " + userId);
            }

            var media = await _mediaService.GetAllAsync(userId);
            return Ok(media);
        }

        [HttpGet("getMediaById")]
        public async Task<IActionResult> GetMedia(Guid id)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            var request = new GetMediaByIdRequestViewModel { Id = id, UserId = userId };
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
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            request.UserId = userId;
            var media = await _mediaService.AddAsync(request, userId);
            return Ok(media);
        }

        [HttpDelete("deleteById")]
        public async Task<IActionResult> DeleteMedia(Guid id)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User not authenticated.");
            }

            try
            {
                var deletedMedia = await _deleteMediaUseCase.DeleteAsync(id, userId);
                return Ok(deletedMedia);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpDelete("deleteUser")]
        public async Task<IActionResult> DeleteAccount()
        {
            try
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User not authenticated." });
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    Console.WriteLine($"Retrieved UserId from token: {userId}");
                    return NotFound(new { message = "User not found." });
                }

                var allMedia = await _mediaService.GetAllAsync(userId);
                if (allMedia != null)
                {
                    foreach (var media in allMedia)
                    {
                        await _deleteMediaUseCase.DeleteAsync(media.Id, userId);
                    }
                }

                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return Ok(
                        new
                        {
                            message = "User account and all associated media deleted successfully.",
                        }
                    );
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return BadRequest(new { message = $"Failed to delete user: {errors}" });
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new { message = $"An error occurred: {e.Message}" });
            }
        }
    }
}
