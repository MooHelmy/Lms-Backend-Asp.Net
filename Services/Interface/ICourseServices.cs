using LMS.Application.DTOs.Courses;
using LMS.Domain.Entities;

public interface ICourseServices
{
    Task<ServicesResponse<CourseDetailsDto?>> GetCourseWithDetailsAsync(int courseId);
    Task<ServicesResponse<IEnumerable<Course>>> GetCoursesByInstructorAsync(int instructorId);
    Task<ServicesResponse<IEnumerable<Course>>> GetTopSellingCoursesAsync(int count);
    Task<ServicesResponse> IsOwnedByInstructorAsync(int courseId, int instructorId);
    Task<ServicesResponse> GetPublishedCoursesCountAsync();
}