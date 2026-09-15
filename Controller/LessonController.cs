using LMS.Api.Controllers;
using LMS.Application.DTOs.Lessons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
[Route("api/lessons")]
[Authorize]
public class LessonController(ILessonServices lessonServices) : BaseApiController
{
    [Authorize(Roles = "Instructor")]
    [HttpPost]
    public async Task<IActionResult> AddLesson(int instructorId, LessonCreateDto dto)
    {
        var result = await lessonServices.AddLessonAsync(instructorId, dto);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Instructor")]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateLesson(int lessonId, int instructorId, LessonUpdateDto dto)
    {
        var result = await lessonServices.UpdateLessonAsync(lessonId, instructorId, dto);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Instructor")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteLesson(int lessonId, int instructorId)
    {
        var result = await lessonServices.DeleteLessonAsync(lessonId, instructorId);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Student")]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetLessonForStudent(int lessonId, int studentId)
    {
        var result = await lessonServices.GetLessonForStudentAsync(lessonId, studentId);
        return HandleResponse(result);
    }
    [AllowAnonymous]
    [HttpGet("course/{courseId:int}/duration")]
    public async Task<IActionResult> GetTotalDurationByCourse(int courseId)
    {
        var result = await lessonServices.GetTotalDurationByCourseAsync(courseId);
        return HandleResponse(result);
    }
    [AllowAnonymous]
    [HttpGet("course/{courseId:int}/count")]
    public async Task<IActionResult> CountLessonsByCourse(int courseId)
    {
        var result = await lessonServices.CountLessonsByCourseAsync(courseId);
        return HandleResponse(result);
    }
    [Authorize(Roles = "Instructor")]
    [HttpGet("section/{sectionId:int}")]
    public async Task<IActionResult> GetLessonsBySection(int sectionId)
    {
        var result = await lessonServices.GetLessonsBySectionAsync(sectionId);
        return HandleResponse(result);
    }

}