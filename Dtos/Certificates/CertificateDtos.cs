namespace LMS.Application.DTOs.Certificates
{
    public class CertificateResponseDto
    {
        public int Id { get; set; }
        public string CertificateNumber { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string CourseTitle { get; set; } = string.Empty;
        public string InstructorName { get; set; } = string.Empty;
        public string? FileUrl { get; set; }
        public DateTime IssuedAt { get; set; }
    }

    public class VerifyCertificateDto
    {
        public bool IsValid { get; set; }
        public CertificateResponseDto? Certificate { get; set; }
    }
}
