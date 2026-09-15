using LMS.Application.DTOs.Certificates;
using LMS.Domain.Entities;

public interface ICertificateServices
{
    Task<ServicesResponse<Certificate?>> GetByCertificateNumberAsync(string certificateNumber);
    Task<ServicesResponse<IEnumerable<Certificate>>> GetCertificatesByStudentAsync(String studentId);
    Task<ServicesResponse> ExistsForStudentCourseAsync(String studentId, int courseId);

    Task<ServicesResponse<Certificate>> GenerateCertificateAsync(String studentId, int courseId);
    Task<ServicesResponse<VerifyCertificateDto>> VerifyAsync(string certificateNumber);
}