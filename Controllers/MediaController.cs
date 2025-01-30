using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WatchBin.Domain.Repositories;
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
            return User.FindFirst(ClaimTypes.Email)?.Value;
        }

        [HttpGet("getAllMedia")]
        public async Task<IActionResult> GetAllMedia()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("id " + userId);
            }

            var media = await _mediaService.GetAllAsync(userId); // Updated to filter by UserId
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

            var request = new GetMediaByIdRequestViewModel { Id = id, UserId = userId }; // Include UserId
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

            request.UserId = userId; // Include UserId
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
                var deletedMedia = await _deleteMediaUseCase.DeleteAsync(id, userId); // Include UserId
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
                // Get the current user's email from the claims
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized(new { message = "User not authenticated." });
                }

                // Find the user by email
                var user = await _userManager.FindByEmailAsync(userId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                // Get all media associated with the user
                var allMedia = await _mediaService.GetAllAsync(userId);
                if (allMedia != null)
                {
                    // Delete each media item
                    foreach (var media in allMedia)
                    {
                        await _deleteMediaUseCase.DeleteAsync(media.Id, userId);
                    }
                }

                // Delete the user
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
                    // Return the errors that occurred during deletion
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
