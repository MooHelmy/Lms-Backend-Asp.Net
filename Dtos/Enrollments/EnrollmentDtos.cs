namespace LMS.Application.DTOs.Enrollments
{
    public class EnrollDto
    {
        public int CourseId { get; set; }
    }

    public class EnrollmentResponseDto
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseTitle { get; set; } = string.Empty;
        public DateTime EnrolledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
        public string Status { get; set; } = string.Empty;
        public int ProgressPercentage { get; set; }
    }
}
