using LMS.Api.Controllers;
using LMS.Application.DTOs.Exams;
using Microsoft.AspNetCore.Mvc;

public class QuestionController(IQuestionServices questionServices) : BaseApiController
{
    public async Task<IActionResult> GetQuestionsByExam(int examId)
    {
        var result = await questionServices.GetQuestionsByExamAsync(examId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetTotalPoints(int examId)
    {
        var result = await questionServices.GetTotalPointsAsync(examId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> GetCorrectAnswer(int questionId)
    {
        var result = await questionServices.GetCorrectAnswerAsync(questionId);
        return HandleResponse(result);
    }
    public async Task<IActionResult> AddQuestion(int examId, int instructorId, QuestionCreateDto dto)
    {
        var result = await questionServices.AddQuestionAsync(examId, instructorId, dto);
        return HandleResponse(result);
    }
}