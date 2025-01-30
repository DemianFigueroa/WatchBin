using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WatchBin.Infrastructure.Entity;
using WatchBin.Services;
using WatchBin.TokenService;
using WatchBin.Users;
using WatchBin.ViewModels;

namespace WatchBin.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMediaService _mediaService;

        public AccountController(
            UserManager<AppUser> userManager,
            ITokenService tokenService,
            SignInManager<AppUser> signInManager,
            IMediaService mediaService
        )
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _mediaService = mediaService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var user = await _userManager.Users.FirstOrDefaultAsync(x =>
                x.UserName == loginDto.Username.ToLower()
            );
            if (user == null)
                return Unauthorized(new { message = "This username does not exist" });
            var resut = await _signInManager.CheckPasswordSignInAsync(
                user,
                loginDto.Password,
                false
            );
            if (!resut.Succeeded)
                return Unauthorized(new { message = "Invalid username and/or password" });
            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user),
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                // Check for validation errors in the model state
                if (!ModelState.IsValid)
                {
                    // Collect validation errors and return them in a consistent format
                    var errors = string.Join(
                        ", ",
                        ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                    );
                    return Unauthorized(new { message = $"Validation failed: {errors}" });
                }

                // Check if the username already exists (if needed)
                var existingUser = await _userManager.FindByNameAsync(registerDto.Username);
                if (existingUser != null)
                {
                    return Unauthorized(new { message = "This username already exists." });
                }

                // Create new user object
                var appUser = new AppUser
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email,
                };

                // Validate password is not empty
                if (string.IsNullOrEmpty(registerDto.Password))
                {
                    return Unauthorized(new { message = "Password cannot be null or empty" });
                }

                // Create user
                var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var userId = appUser.Email;
                    var templateMedia = MediaTemplate.GetDefaultMedia(userId); // Get default media
                    foreach (var media in templateMedia)
                    {
                        await _mediaService.AddAsync(media, userId); // Add each media item
                    }

                    // Assign role to user
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Username = appUser.UserName,
                                Email = appUser.Email,
                                Token = _tokenService.CreateToken(appUser),
                            }
                        );
                    }
                    else
                    {
                        return Unauthorized(new { message = "Failed to assign role to user" });
                    }
                }
                else
                {
                    // Return the errors that occurred during user creation
                    var errors = string.Join(", ", createdUser.Errors.Select(e => e.Description));
                    return Unauthorized(new { message = $"Failed to create user: {errors}" });
                }
            }
            catch (Exception e)
            {
                return Unauthorized(new { message = $"An error occurred: {e.Message}" });
            }
        }
    }
}
