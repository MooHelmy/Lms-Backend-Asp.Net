using LMS.Api.Controllers;
using LMS.Application.DTOs.ExamAttempts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/exam-attempts")]
[Authorize]
public class ExamAttemptController(IExamAttemptServices examAttemptServices) : BaseApiController
{
    [HttpGet("student/{studentId}/exam/{examId}")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> GetAttemptsByStudent(int studentId, int examId)
    {
        var result = await examAttemptServices.GetAttemptsByStudentAsync(studentId, examId);
        return HandleResponse(result);
    }
    [HttpGet("{attemptId}/answers")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> GetAttemptWithAnswers(int attemptId)
    {
        var result = await examAttemptServices.GetAttemptWithAnswersAsync(attemptId);
        return HandleResponse(result);
    }
    [HttpGet("student/{studentId}/exam/{examId}/count")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> GetAttemptsCount(int studentId, int examId)
    {
        var result = await examAttemptServices.GetAttemptsCountAsync(studentId, examId);
        return HandleResponse(result);
    }
    [HttpGet("student/{studentId}/exam/{examId}/latest")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> GetLatestAttempt(int studentId, int examId)
    {
        var result = await examAttemptServices.GetLatestAttemptAsync(studentId, examId);
        return HandleResponse(result);
    }
    [HttpGet("student/{studentId}/exam/{examId}/passed")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> HasPassed(int studentId, int examId)
    {
        var result = await examAttemptServices.HasPassedAsync(studentId, examId);
        return HandleResponse(result);
    }
    [HttpPost("student/{studentId}/start")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> StartAttempt(int studentId, StartExamAttemptDto dto)
    {
        var result = await examAttemptServices.StartAttemptAsync(studentId, dto);
        return HandleResponse(result);
    }
    [HttpPost("student/{studentId}/submit")]
    [Authorize(Roles = "student")]
    public async Task<IActionResult> SubmitAttempt(int studentId, SubmitExamAttemptDto dto)
    {
        var result = await examAttemptServices.SubmitAttemptAsync(studentId, dto);
        return HandleResponse(result);
    }
}