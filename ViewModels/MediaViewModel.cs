namespace WatchBin.ViewModels
{
    public class MediaViewModel
    {
        public string UserId { get; set; } = string.Empty;
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool Status { get; set; }
    }
}
