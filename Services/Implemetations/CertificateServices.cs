using LMS.Domain.Entities;

public class CertificateServices(ICertificateRepository certificateRepository) : ICertificateServices
{
    public async Task<ServicesResponse<Certificate?>> GetByCertificateNumberAsync(string certificateNumber)
    {
        var certificate = await certificateRepository.GetByCertificateNumberAsync(certificateNumber);
        if (certificate == null)
        {
            return new ServicesResponse<Certificate?>(false, "No certificate found for the given certificate number.");
        }
        return new ServicesResponse<Certificate?>(true, "Certificate found for the given certificate number.", certificate);
    }

    public async Task<ServicesResponse<IEnumerable<Certificate>>> GetCertificatesByStudentAsync(int studentId)
    {
        var certificates = await certificateRepository.GetCertificatesByStudentAsync(studentId);
        if (certificates == null || !certificates.Any())
        {
            return new ServicesResponse<IEnumerable<Certificate>>(false, "No certificates found for the given student id.");
        }
        return new ServicesResponse<IEnumerable<Certificate>>(true, "Certificates found for the given student id.", certificates);
    }

    public async Task<ServicesResponse> ExistsForStudentCourseAsync(int studentId, int courseId)
    {
        var exists = await certificateRepository.ExistsForStudentCourseAsync(studentId, courseId);
        if (!exists)
        {
            return new ServicesResponse(false, "Certificate not found for the given student id and course id.");
        }
        return new ServicesResponse(true, "Certificate found for the given student id and course id.");
    }
}