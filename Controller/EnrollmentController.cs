using LMS.Api.Controllers;
using LMS.Application.DTOs.Enrollments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/enrollments")]
[Authorize]
public class EnrollmentController(IEnrollmentServices enrollmentServices) : BaseApiController
{
    [HttpGet("is-enrolled")]
    [AllowAnonymous]
    public async Task<IActionResult> IsEnrolled(String studentId, int courseId)
    {
        var result = await enrollmentServices.IsEnrolledAsync(studentId, courseId);
        return HandleResponse(result);
    }
    [HttpGet("student/{studentId}")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> GetEnrollmentsByStudent(String studentId)
    {
        var result = await enrollmentServices.GetEnrollmentsByStudentAsync(studentId);
        return HandleResponse(result);
    }
    [HttpGet("course/{courseId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetEnrollmentsByCourse(int courseId)
    {
        var result = await enrollmentServices.GetEnrollmentsByCourseAsync(courseId);
        return HandleResponse(result);
    }
    [HttpGet("course/{courseId}/active-count")]
    [AllowAnonymous]
    public async Task<IActionResult> GetActiveEnrollmentsCount(int courseId)
    {
        var result = await enrollmentServices.GetActiveEnrollmentsCountAsync(courseId);
        return HandleResponse(result);
    }
    [HttpPut("enrollments/{enrollmentId}/completed")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> MarkAsCompleted(int enrollmentId)
    {
        var result = await enrollmentServices.MarkAsCompletedAsync(enrollmentId);
        return HandleResponse(result);
    }
    [HttpPost("enrollments")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> Enroll(String studentId, EnrollDto dto)
    {
        var result = await enrollmentServices.EnrollAsync(studentId, dto);
        return HandleResponse(result);
    }
    [HttpDelete("enrollments")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> CancelEnrollment(String studentId, int courseId)
    {
        var result = await enrollmentServices.CancelEnrollmentAsync(studentId, courseId);
        return HandleResponse(result);
    }
}