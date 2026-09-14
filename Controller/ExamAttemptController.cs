using LMS.Api.Controllers;
using LMS.Application.DTOs.ExamAttempts;
using Microsoft.AspNetCore.Mvc;

public class ExamAttemptController(IExamAttemptServices examAttemptServices) : BaseApiController
{
    public async Task<IActionResult> GetAttemptsByStudent(int studentId, int examId)
    {
        var result = await examAttemptServices.GetAttemptsByStudentAsync(studentId, examId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetAttemptWithAnswers(int attemptId)
    {
        var result = await examAttemptServices.GetAttemptWithAnswersAsync(attemptId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetAttemptsCount(int studentId, int examId)
    {
        var result = await examAttemptServices.GetAttemptsCountAsync(studentId, examId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetLatestAttempt(int studentId, int examId)
    {
        var result = await examAttemptServices.GetLatestAttemptAsync(studentId, examId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> HasPassed(int studentId, int examId)
    {
        var result = await examAttemptServices.HasPassedAsync(studentId, examId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> StartAttempt(int studentId, StartExamAttemptDto dto)
    {
        var result = await examAttemptServices.StartAttemptAsync(studentId, dto);
        return HandleResponse(result);
    }
    public async Task<IActionResult> SubmitAttempt(int studentId, SubmitExamAttemptDto dto)
    {
        var result = await examAttemptServices.SubmitAttemptAsync(studentId, dto);
        return HandleResponse(result);
    }
}