using LMS.Application.DTOs.Exams;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Api.Controllers;

[Route("api/exams")]
[Authorize]
public class ExamsController(IExamServices examServices) : BaseApiController
{
    // GET /api/exams/course/5 - كل امتحانات كورس معين
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetExamsByCourse(int courseId)
    {
        var result = await examServices.GetExamsByCourseAsync(courseId);
        return HandleResponse(result);
    }

    // GET /api/exams/5 - الامتحان مع كل الأسئلة والإجابات (فيها IsCorrect، للمدرس/الأدمن بس)
    [Authorize(Roles = "Instructor,Admin")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetExamWithQuestions(int id)
    {
        var result = await examServices.GetExamWithQuestionsAsync(id);
        return HandleResponse(result);
    }

    // GET /api/exams/5/is-owned/3 - تأكيد ملكية الامتحان لمدرس معين
    [Authorize(Roles = "Instructor")]
    [HttpGet("{examId:int}/is-owned/{instructorId:int}")]
    public async Task<IActionResult> IsOwnedByInstructor(int examId, int instructorId)
    {
        var result = await examServices.IsOwnedByInstructorAsync(examId, instructorId);
        return HandleResponse(result);
    }

    // POST /api/exams - إنشاء امتحان جديد لكورس، للـ Instructor بس
    [Authorize(Roles = "Instructor")]
    [HttpPost]
    public async Task<IActionResult> CreateExam(ExamCreateDto dto)
    {
        var result = await examServices.CreateExamAsync(CurrentUserId, dto);
        return HandleResponse(result);
    }

    // PUT /api/exams/5
    [Authorize(Roles = "Instructor")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateExam(int id, ExamUpdateDto dto)
    {
        var result = await examServices.UpdateExamAsync(id, CurrentUserId, dto);
        return HandleResponse(result);
    }

    // DELETE /api/exams/5
    [Authorize(Roles = "Instructor")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteExam(int id)
    {
        var result = await examServices.DeleteExamAsync(id, CurrentUserId);
        return HandleResponse(result);
    }
}
