using LMS.Application.DTOs.Certificates;
using LMS.Application.Mappers;
using LMS.Domain.Entities;

public class CertificateServices(ICertificateRepository certificateRepository) : ICertificateServices
{
    // بيصدر شهادة جديدة للطالب على الكورس، أو يرجع الشهادة الموجودة لو صدرت قبل كده (منع تكرار)
    public async Task<ServicesResponse<Certificate>> GenerateCertificateAsync(int studentId, int courseId)
    {
        var alreadyExists = await certificateRepository.ExistsForStudentCourseAsync(studentId, courseId);
        if (alreadyExists)
        {
            var existing = await certificateRepository.SingleOrDefaultAsync(
                c => c.StudentId == studentId && c.CourseId == courseId);

            return new ServicesResponse<Certificate>(true, "Certificate already issued.", existing);
        }

        var certificate = new Certificate
        {
            StudentId = studentId,
            CourseId = courseId,
            CertificateNumber = $"LMS-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}",
            IssuedAt = DateTime.UtcNow
        };

        await certificateRepository.CreateAsync(certificate);

        return new ServicesResponse<Certificate>(true, "Certificate generated successfully.", certificate);
    }

    // بيتحقق من صحة رقم شهادة معين - مستخدمة في الـ Verify Endpoint العام
    public async Task<ServicesResponse<VerifyCertificateDto>> VerifyAsync(string certificateNumber)
    {
        var certificate = await certificateRepository.GetByCertificateNumberAsync(certificateNumber);
        var result = certificate.CertificateToVerifyMapper();

        var message = result.IsValid ? "Certificate is valid." : "Certificate not found.";
        return new ServicesResponse<VerifyCertificateDto>(true, message, result);
    }


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