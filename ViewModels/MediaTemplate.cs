namespace WatchBin.ViewModels
{
    public static class MediaTemplate
    {
        public static List<AddMediaRequestViewModel> GetDefaultMedia(string userId)
        {
            return new List<AddMediaRequestViewModel>
            {
                new AddMediaRequestViewModel
                {
                    Name = "Breaking Bad",
                    Type = "Show",
                    Creator = "Vince Gilligan",
                    ReleaseDate = new DateTime(2008, 1, 20),
                    Description =
                        "A high school chemistry teacher turned methamphetamine producer navigates the drug trade.",
                    CoverImage =
                        "https://aninnovativepursuit.wordpress.com/wp-content/uploads/2015/02/ac7b4-breaking-bad-poster-season-2-large.jpg",
                    Category = "Drama",
                    Status = false,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "Half-Life",
                    Type = "Game",
                    Creator = "Valve",
                    ReleaseDate = new DateTime(1998, 11, 19),
                    Description =
                        "A first-person shooter that redefined the genre with its immersive storytelling.",
                    CoverImage =
                        "https://upload.wikimedia.org/wikipedia/en/f/fa/Half-Life_Cover_Art.jpg",
                    Category = "Science Fiction",
                    Status = true,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "Half-Life 2",
                    Type = "Game",
                    Creator = "Valve",
                    ReleaseDate = new DateTime(2004, 11, 16),
                    Description =
                        "The sequel to Half-Life, praised for its physics-based gameplay and immersive world.",
                    CoverImage =
                        "https://upload.wikimedia.org/wikipedia/en/2/25/Half-Life_2_cover.jpg",
                    Category = "Science Fiction",
                    Status = false,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "The Thing",
                    Type = "Movie",
                    Creator = "John Carpenter",
                    ReleaseDate = new DateTime(1982, 6, 25),
                    Description =
                        "A research team in Antarctica is hunted by a shape-shifting alien that assumes the appearance of its victims.",
                    CoverImage =
                        "https://m.media-amazon.com/images/M/MV5BYTA3NDU5MWEtNTk4Yy00ZDNkLThmZTQtMjU3ZGVhYzAyMzU4XkEyXkFqcGc@._V1_.jpg",
                    Category = "Horror",
                    Status = true,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "Black Adder",
                    Type = "Show",
                    Creator = "Richard Curtis, Rowan Atkinson",
                    ReleaseDate = new DateTime(1983, 6, 15),
                    Description =
                        "A satirical historical sitcom following the misadventures of Edmund Blackadder and his dim-witted sidekick Baldrick across different eras of British history.",
                    CoverImage =
                        "https://imageservice.sky.com/contentid/iYsxVQcz3NRZRmXRBpvC6W/BOXART-SEASON?language=eng&territory=GB&proposition=NOWTV&version=97cbb78f-75c1-37da-b9c6-1ee71dc37944",
                    Category = "Comedy",
                    Status = true,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "Akira",
                    Type = "Movie",
                    Creator = "Katsuhiro Otomo",
                    ReleaseDate = new DateTime(1988, 7, 16),
                    Description =
                        "A secret military project endangers Neo-Tokyo when it turns a biker gang member into a rampaging psychic psychopath who can only be stopped by two teenagers and a group of psychics.",
                    CoverImage =
                        "https://m.media-amazon.com/images/I/91SI73USlHL._AC_UF1000,1000_QL80_.jpg",
                    Category = "Anime",
                    Status = true,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "The Great Gatsby",
                    Type = "Book",
                    Creator = "F. Scott Fitzgerald",
                    ReleaseDate = new DateTime(1925, 4, 10),
                    Description =
                        "A novel about the American Dream, set in the Roaring Twenties, exploring themes of decadence, idealism, and social upheaval through the eyes of the mysterious millionaire Jay Gatsby.",
                    CoverImage =
                        "https://m.media-amazon.com/images/I/61z0MrB6qOS._AC_UF894,1000_QL80_.jpg",
                    Category = "Classic Literature",
                    Status = true,
                    UserId = userId,
                },
                new AddMediaRequestViewModel
                {
                    Name = "Alice in Wonderland",
                    Type = "Book",
                    Creator = "Lewis Carroll",
                    ReleaseDate = new DateTime(1865, 11, 26),
                    Description =
                        "A whimsical tale of a young girl named Alice who falls through a rabbit hole into a fantastical world filled with peculiar creatures and surreal adventures.",
                    CoverImage =
                        "https://m.media-amazon.com/images/I/71ahuFargsL._AC_UF894,1000_QL80_.jpg",
                    Category = "Fantasy",
                    Status = true,
                    UserId = userId,
                },
            };
        }
    }
}
