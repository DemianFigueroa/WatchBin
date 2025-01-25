using WatchBin.Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WatchBin.Users;
using Microsoft.AspNetCore.Identity;

namespace WatchBin.Infrastructure.Repositories
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<AppUser>(options)
    {
        public required DbSet<MediaEntity> Media { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = "User", Name = "User", NormalizedName = "USER" }
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<MediaEntity>().HasData(
                new MediaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Breaking Bad",
                    Type = "Show",
                    Creator = "Vince Gilligan",
                    ReleaseDate = new DateTime(2008, 1, 20),
                    Language = "English",
                    Description = "A high school chemistry teacher turned methamphetamine producer navigates the drug trade.",
                    CoverImage = "https://example.com/breakingbad.jpg",
                    Notes = "Critically acclaimed and widely regarded as one of the greatest TV shows.",
                    Category = "Drama",
                    AddedDate = DateTime.Now,
                    Priority = 5,
                    Status = false,
                    CompletionStatus = true,
                    Platform = "Netflix",
                    Length = 5,
                    IMBDCode = "tt0903747"
                },
                new MediaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Half-Life",
                    Type = "Game",
                    Creator = "Valve",
                    ReleaseDate = new DateTime(1998, 11, 19),
                    Language = "English",
                    Description = "A first-person shooter that redefined the genre with its immersive storytelling.",
                    CoverImage = "https://example.com/halflife1.jpg",
                    Notes = "A groundbreaking game in video game history.",
                    Category = "Science Fiction",
                    AddedDate = DateTime.Now,
                    Priority = 3,
                    Status = true,
                    CompletionStatus = true,
                    Platform = "PC",
                    Length = 2,
                    IMBDCode = ""
                },
                new MediaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Half-Life 2",
                    Type = "Game",
                    Creator = "Valve",
                    ReleaseDate = new DateTime(2004, 11, 16),
                    Language = "English",
                    Description = "The sequel to Half-Life, praised for its physics-based gameplay and immersive world.",
                    CoverImage = "https://example.com/halflife2.jpg",
                    Notes = "Frequently listed as one of the greatest games of all time.",
                    Category = "Science Fiction",
                    AddedDate = DateTime.Now,
                    Priority = 4,
                    Status = false,
                    CompletionStatus = true,
                    Platform = "PC",
                    Length = 3,
                    IMBDCode = ""
                },
                new MediaEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "The Thing",
                    Type = "Movie",
                    Creator = "John Carpenter",
                    ReleaseDate = new DateTime(1982, 6, 25),
                    Language = "English",
                    Description = "A research team in Antarctica is hunted by a shape-shifting alien that assumes the appearance of its victims.",
                    CoverImage = "https://example.com/thething.jpg",
                    Notes = "A cult classic in horror and science fiction genres.",
                    Category = "Horror",
                    AddedDate = DateTime.Now,
                    Priority = 4,
                    Status = true,
                    CompletionStatus = true,
                    Platform = "Blu-ray",
                    Length = 2,
                    IMBDCode = "tt0084787"
                }
            );
        }
    }
}