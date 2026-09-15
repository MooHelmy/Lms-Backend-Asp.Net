namespace LMS.Domain.Entities
{
    public class Review : BaseEntity
    {
        public String StudentId { get; set; } = string.Empty;
        public User Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public int Rating { get; set; } // 1-5
        public string? Comment { get; set; }
    }
}
