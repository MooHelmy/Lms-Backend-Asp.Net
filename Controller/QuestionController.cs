using LMS.Api.Controllers;
using LMS.Application.DTOs.Exams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/questions")]
[Authorize]
public class QuestionController(IQuestionServices questionServices) : BaseApiController
{
    [HttpGet("exam/{examId:int}")]
    public async Task<IActionResult> GetQuestionsByExam(int examId)
    {
        var result = await questionServices.GetQuestionsByExamAsync(examId);
        return HandleResponse(result);
    }
    [HttpGet("exam/{examId:int}/total-points")]
    public async Task<IActionResult> GetTotalPoints(int examId)
    {
        var result = await questionServices.GetTotalPointsAsync(examId);
        return HandleResponse(result);
    }
    [HttpGet("{questionId:int}/correct-answer")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> GetCorrectAnswer(int questionId)
    {
        var result = await questionServices.GetCorrectAnswerAsync(questionId);
        return HandleResponse(result);
    }
    [HttpPost("exam/{examId:int}/add-question")]
    [Authorize(Roles = "Instructor,Admin")]
    public async Task<IActionResult> AddQuestion(int examId, String instructorId, QuestionCreateDto dto)
    {
        var result = await questionServices.AddQuestionAsync(examId, instructorId, dto);
        return HandleResponse(result);
    }
}