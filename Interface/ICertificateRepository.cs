using LMS.Domain.Entities;

public interface ICertificateRepository : IGeneric<Certificate>
{
    Task<Certificate?> GetByCertificateNumberAsync(string certificateNumber);
    Task<IEnumerable<Certificate>> GetCertificatesByStudentAsync(int studentId);
    Task<bool> ExistsForStudentCourseAsync(int studentId, int courseId);
}
