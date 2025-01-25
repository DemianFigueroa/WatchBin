using System.ComponentModel.DataAnnotations;
namespace WatchBin.Infrastructure.Entity
{
    public class LoginDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}