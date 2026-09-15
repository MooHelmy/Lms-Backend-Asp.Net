namespace LMS.Domain.Entities
{
    public class Certificate : BaseEntity
    {
        public String StudentId { get; set; } = string.Empty;
        public User Student { get; set; } = null!;

        public int CourseId { get; set; }
        public Course Course { get; set; } = null!;

        public string CertificateNumber { get; set; } = string.Empty;
        public string? FileUrl { get; set; }
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
    }
}
