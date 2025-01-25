namespace WatchBin.Infrastructure.Entity
{
    public class MediaEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Creator { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string Language { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string CoverImage { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
        public int Priority { get; set; }
        public bool Status { get; set; }
        public bool CompletionStatus { get; set; }
        public string Platform { get; set; } = string.Empty;
        public int Length { get; set; }
        public string IMBDCode { get; set; } = string.Empty;
    }
}