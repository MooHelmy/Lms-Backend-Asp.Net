using LMS.Api.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/certificates")]

public class CertificateController(ICertificateServices certificateServices) : BaseApiController
{
    [HttpPost("generate")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GenerateCertificate(String studentId, int courseId)
    {
        var result = await certificateServices.GenerateCertificateAsync(studentId, courseId);
        return HandleResponse(result);
    }
    [AllowAnonymous]
    [HttpGet("verify")]

    public async Task<IActionResult> Verify(string certificateNumber)
    {
        var result = await certificateServices.VerifyAsync(certificateNumber);
        return HandleResponse(result);
    }

    [HttpGet("by-number")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByCertificateNumber(string certificateNumber)
    {
        var result = await certificateServices.GetByCertificateNumberAsync(certificateNumber);
        return HandleResponse(result);
    }

    [HttpGet("by-student")]
    [Authorize(Roles = "Student, Admin")]
    public async Task<IActionResult> GetCertificatesByStudent(String studentId)
    {
        var result = await certificateServices.GetCertificatesByStudentAsync(studentId);
        return HandleResponse(result);
    }
    [HttpGet("exists")]
    [Authorize(Roles = "Student, Admin")]
    public async Task<IActionResult> ExistsForStudentCourse(String studentId, int courseId)
    {
        var result = await certificateServices.ExistsForStudentCourseAsync(studentId, courseId);
        return HandleResponse(result);
    }
}