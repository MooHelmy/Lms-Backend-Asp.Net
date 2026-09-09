namespace LMS.Domain.Entities
{
    public class Lesson : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public LessonContentType ContentType { get; set; }
        public string? VideoUrl { get; set; }
        public int DurationInSeconds { get; set; }
        public int Order { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; } = null!;

        public ICollection<LessonProgress> ProgressRecords { get; set; } = new List<LessonProgress>();
    }
}
