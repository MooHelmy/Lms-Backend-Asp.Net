using LMS.Api.Controllers;
using LMS.Application.DTOs.Courses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/courses")]
[Authorize]
public class CourseController(ICourseServices courseServices) : BaseApiController
{
    [HttpGet("courses")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourses(CourseFilterDto filter)
    {
        var result = await courseServices.GetCoursesAsync(filter);
        return HandleResponse(result);
    }
    [HttpGet("courses/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCourseById(int id)
    {
        var result = await courseServices.GetCourseWithDetailsAsync(id);
        return HandleResponse(result);
    }
    [HttpPost("courses")]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> CreateCourse(int instructorId, CourseCreateDto dto)
    {
        var result = await courseServices.CreateCourseAsync(instructorId, dto);
        return HandleResponse(result);
    }
    [HttpPut("courses/{id}")]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> UpdateCourse(int id, CourseUpdateDto dto, int instructorId)
    {
        var result = await courseServices.UpdateCourseAsync(id, instructorId, dto);
        return HandleResponse(result);
    }
    [HttpDelete("courses/{id}")]
    [Authorize(Roles = "Admin,Instructor")]
    public async Task<IActionResult> DeleteCourse(int id, int instructorId)
    {
        var result = await courseServices.DeleteCourseAsync(id, instructorId);
        return HandleResponse(result);
    }
    [HttpGet("courses/published")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PublishCourse()
    {
        var result = await courseServices.GetPublishedCoursesCountAsync();
        return HandleResponse(result);
    }
    [HttpGet("courses/instructor/{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetCoursesByInstructor(int id)
    {
        var result = await courseServices.GetCoursesByInstructorAsync(id);
        return HandleResponse(result);
    }
    [HttpGet("courses/top-selling")]
    [AllowAnonymous]
    public async Task<IActionResult> GetTopSellingCourses(int count)
    {
        var result = await courseServices.GetTopSellingCoursesAsync(count);
        return HandleResponse(result);
    }
}