namespace LMS.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public String StudentId { get; set; } = string.Empty;
        public User Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    }
}
