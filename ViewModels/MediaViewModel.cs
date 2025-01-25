namespace WatchBin.ViewModels
{
    public class MediaViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string CoverImage { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public bool CompletionStatus { get; set; }
        public int Length { get; set; }
    }
}