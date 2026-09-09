namespace LMS.Domain.Entities
{
    public class LessonProgress : BaseEntity
    {
        public int StudentId { get; set; }
        public User Student { get; set; } = null!;

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; } = null!;

        public bool IsCompleted { get; set; }
        public int WatchedSeconds { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
