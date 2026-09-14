using LMS.Api.Controllers;
using LMS.Application.DTOs.Enrollments;
using Microsoft.AspNetCore.Mvc;

public class EnrollmentController(IEnrollmentServices enrollmentServices) : BaseApiController
{
    public async Task<IActionResult> IsEnrolled(int studentId, int courseId)
    {
        var result = await enrollmentServices.IsEnrolledAsync(studentId, courseId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetEnrollmentsByStudent(int studentId)
    {
        var result = await enrollmentServices.GetEnrollmentsByStudentAsync(studentId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetEnrollmentsByCourse(int courseId)
    {
        var result = await enrollmentServices.GetEnrollmentsByCourseAsync(courseId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetActiveEnrollmentsCount(int courseId)
    {
        var result = await enrollmentServices.GetActiveEnrollmentsCountAsync(courseId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> MarkAsCompleted(int enrollmentId)
    {
        var result = await enrollmentServices.MarkAsCompletedAsync(enrollmentId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> Enroll(int studentId, EnrollDto dto)
    {
        var result = await enrollmentServices.EnrollAsync(studentId, dto);
        return HandleResponse(result);
    }
    public async Task<IActionResult> CancelEnrollment(int studentId, int courseId)
    {
        var result = await enrollmentServices.CancelEnrollmentAsync(studentId, courseId);
        return HandleResponse(result);
    }
}