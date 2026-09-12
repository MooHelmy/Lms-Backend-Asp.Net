using LMS.Domain.Entities;

public interface ICertificateServices
{
    Task<ServicesResponse<Certificate?>> GetByCertificateNumberAsync(string certificateNumber);
    Task<ServicesResponse<IEnumerable<Certificate>>> GetCertificatesByStudentAsync(int studentId);
    Task<ServicesResponse> ExistsForStudentCourseAsync(int studentId, int courseId);
}
