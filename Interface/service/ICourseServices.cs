using LMS.Application.DTOs.Common;
using LMS.Application.DTOs.Courses;
using LMS.Domain.Entities;

public interface ICourseServices
{
    Task<ServicesResponse<PagedResult<CourseListItemDto>>> GetCoursesAsync(CourseFilterDto filter); Task<ServicesResponse<CourseDetailsDto?>> GetCourseWithDetailsAsync(int courseId);
    Task<ServicesResponse<IEnumerable<Course>>> GetCoursesByInstructorAsync(int instructorId);
    Task<ServicesResponse<IEnumerable<Course>>> GetTopSellingCoursesAsync(int count);
    Task<ServicesResponse<bool>> IsOwnedByInstructorAsync(int courseId, int instructorId);
    Task<ServicesResponse<int>> GetPublishedCoursesCountAsync();
    Task<ServicesResponse<CourseDetailsDto>> CreateCourseAsync(int instructorId, CourseCreateDto dto);
    Task<ServicesResponse<bool>> UpdateCourseAsync(int courseId, int instructorId, CourseUpdateDto dto);
    Task<ServicesResponse<bool>> DeleteCourseAsync(int courseId, int instructorId);
}
