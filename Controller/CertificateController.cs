using LMS.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

public class CertificateController(ICertificateServices certificateServices) : BaseApiController
{
    public async Task<IActionResult> GenerateCertificate(int studentId, int courseId)
    {
        var result = await certificateServices.GenerateCertificateAsync(studentId, courseId);
        return HandleResponse(result);
    }

    public async Task<IActionResult> Verify(string certificateNumber)
    {
        var result = await certificateServices.VerifyAsync(certificateNumber);
        return HandleResponse(result);
    }

    public async Task<IActionResult> GetByCertificateNumber(string certificateNumber)
    {
        var result = await certificateServices.GetByCertificateNumberAsync(certificateNumber);
        return HandleResponse(result);
    }

    public async Task<IActionResult> GetCertificatesByStudent(int studentId)
    {
        var result = await certificateServices.GetCertificatesByStudentAsync(studentId);
        return HandleResponse(result);
    }

    public async Task<IActionResult> ExistsForStudentCourse(int studentId, int courseId)
    {
        var result = await certificateServices.ExistsForStudentCourseAsync(studentId, courseId);
        return HandleResponse(result);
    }
}