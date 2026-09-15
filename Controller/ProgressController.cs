using LMS.Api.Controllers;
using LMS.Application.DTOs.Progress;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/progress")]
[Authorize(Roles = "Student")]
public class ProgressController(IProgressServices progressServices) : BaseApiController
{
    [HttpGet("lesson/{lessonId:int}")]
    public async Task<IActionResult> GetProgress(String studentId, int lessonId)
    {
        var result = await progressServices.GetProgressAsync(studentId, lessonId);
        return HandleResponse(result);
    }
    [HttpGet("course/{courseId:int}/completed")]
    public async Task<IActionResult> GetCompletedLessonsCount(String studentId, int courseId)
    {
        var result = await progressServices.GetCompletedLessonsCountAsync(studentId, courseId);
        return HandleResponse(result);
    }
    [HttpPatch("lesson/{lessonId:int}/complete")]
    public async Task<IActionResult> MarkLessonCompleted(String studentId, int lessonId)
    {
        var result = await progressServices.MarkLessonCompletedAsync(studentId, lessonId);
        return HandleResponse(result);
    }
    [HttpPut("lesson/{lessonId:int}")]
    public async Task<IActionResult> UpdateLessonProgress(String studentId, UpdateLessonProgressDto dto)
    {
        var result = await progressServices.UpdateLessonProgressAsync(studentId, dto);
        return HandleResponse(result);
    }
    [HttpGet("course/{courseId:int}")]
    public async Task<IActionResult> GetCourseProgress(String studentId, int courseId)
    {
        var result = await progressServices.GetCourseProgressAsync(studentId, courseId);
        return HandleResponse(result);
    }
}