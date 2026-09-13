using LMS.Application.DTOs.Certificates;
using LMS.Domain.Entities;

public interface ICertificateServices
{
    Task<ServicesResponse<Certificate?>> GetByCertificateNumberAsync(string certificateNumber);
    Task<ServicesResponse<IEnumerable<Certificate>>> GetCertificatesByStudentAsync(int studentId);
    Task<ServicesResponse> ExistsForStudentCourseAsync(int studentId, int courseId);

    Task<ServicesResponse<Certificate>> GenerateCertificateAsync(int studentId, int courseId);
    Task<ServicesResponse<VerifyCertificateDto>> VerifyAsync(string certificateNumber);
}