using LMS.Domain.Entities;

public interface ICertificateRepository : IGeneric<Certificate>
{
    Task<Certificate?> GetByCertificateNumberAsync(string certificateNumber);
    Task<IEnumerable<Certificate>> GetCertificatesByStudentAsync(String studentId);
    Task<bool> ExistsForStudentCourseAsync(String studentId, int courseId);
}
