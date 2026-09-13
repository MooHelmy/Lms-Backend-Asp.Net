using LMS.Application.DTOs.Courses;
using Microsoft.AspNetCore.Mvc;
[ApiController]
[Route("api/courses")]
public class CourseController(ICourseServices courseServices) : ControllerBase
{
    public async Task<IActionResult> GetCourses(CourseFilterDto filter)
    {
        var result = await courseServices.GetCoursesAsync(filter);
        if (result.Data == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    public async Task<IActionResult> GetCourseById(int id)
    {
        var result = await courseServices.GetCourseWithDetailsAsync(id);
        if (result.Data == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    public async Task<IActionResult> CreateCourse(int instructorId, CourseCreateDto dto)
    {
        var result = await courseServices.CreateCourseAsync(instructorId, dto);
        return Ok(result);
    }
    public async Task<IActionResult> UpdateCourse(int id, CourseUpdateDto dto, int instructorId)
    {
        var result = await courseServices.UpdateCourseAsync(id, instructorId, dto);
        return Ok(result);
    }
    public async Task<IActionResult> DeleteCourse(int id, int instructorId)
    {
        var result = await courseServices.DeleteCourseAsync(id, instructorId);
        return Ok(result);
    }
    public async Task<IActionResult> PublishCourse()
    {
        var result = await courseServices.GetPublishedCoursesCountAsync();
        return Ok(result);
    }
    public async Task<IActionResult> GetCoursesByInstructor(int id)
    {
        var result = await courseServices.GetCoursesByInstructorAsync(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    public async Task<IActionResult> GetTopSellingCourses(int count)
    {
        var result = await courseServices.GetTopSellingCoursesAsync(count);
        return Ok(result);
    }
}