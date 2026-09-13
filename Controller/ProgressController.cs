using LMS.Application.DTOs.Progress;
using Microsoft.AspNetCore.Mvc;

public class ProgressController(IProgressServices progressServices) : ControllerBase
{
    public async Task<IActionResult> GetProgress(int studentId, int lessonId)
    {
        var result = await progressServices.GetProgressAsync(studentId, lessonId);
        return Ok(result);
    }
    public async Task<IActionResult> GetCompletedLessonsCount(int studentId, int courseId)
    {
        var result = await progressServices.GetCompletedLessonsCountAsync(studentId, courseId);
        return Ok(result);
    }
    public async Task<IActionResult> MarkLessonCompleted(int studentId, int lessonId)
    {
        var result = await progressServices.MarkLessonCompletedAsync(studentId, lessonId);
        return Ok(result);
    }
    public async Task<IActionResult> UpdateLessonProgress(int studentId, UpdateLessonProgressDto dto)
    {
        var result = await progressServices.UpdateLessonProgressAsync(studentId, dto);
        return Ok(result);
    }
    public async Task<IActionResult> GetCourseProgress(int studentId, int courseId)
    {
        var result = await progressServices.GetCourseProgressAsync(studentId, courseId);
        return Ok(result);
    }
}