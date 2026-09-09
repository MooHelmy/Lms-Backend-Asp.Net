namespace LMS.Domain.Entities
{
    public class Enrollment : BaseEntity
    {
        public int StudentId { get; set; }
        public User Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
        public DateTime? CompletedAt { get; set; }
        public EnrollmentStatus Status { get; set; } = EnrollmentStatus.Active;
    }
}
